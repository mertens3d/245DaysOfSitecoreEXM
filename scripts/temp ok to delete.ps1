

	$solrName = "solr-7.2.1-for-sc9.1.1-raw"
	$solrHost = "solr-7.2.1-for-sc9.1.1-raw"
    $solrRoot = "C:\solr\solr-7.2.1-for-sc9.1.1-raw"

    # Generate SSL cert
    $existingCert = Get-ChildItem Cert:\LocalMachine\Root | where FriendlyName -eq "$solrName"
    if(!($existingCert))
    {
        Write-Host "Creating & trusting an new SSL Cert for $solrHost"

        # Generate a cert
        # https://docs.microsoft.com/en-us/powershell/module/pkiclient/new-selfsignedcertificate?view=win10-ps
        $cert = New-SelfSignedCertificate -FriendlyName "$solrName" -DnsName "$solrHost" -CertStoreLocation "cert:\LocalMachine" -NotAfter (Get-Date).AddYears(10)

        # Trust the cert
        # https://stackoverflow.com/questions/8815145/how-to-trust-a-certificate-in-windows-powershell
        $store = New-Object System.Security.Cryptography.X509Certificates.X509Store "Root","LocalMachine"
        $store.Open("ReadWrite")
        $store.Add($cert)
        $store.Close()

        # remove the untrusted copy of the cert
        $cert | Remove-Item
    }

    # export the cert to pfx using solr's default password
    if(!(Test-Path -Path "$solrRoot\server\etc\solr-ssl.keystore.pfx"))
    {
        Write-Host "Exporting cert for Solr to use"

        $cert = Get-ChildItem Cert:\LocalMachine\Root | where FriendlyName -eq "$solrName"
    
        $certStore = "$solrRoot\server\etc\solr-ssl.keystore.pfx"
        $certPwd = ConvertTo-SecureString -String "secret" -Force -AsPlainText
        $cert | Export-PfxCertificate -FilePath $certStore -Password $certpwd | Out-Null
    }

    # Update solr cfg to use keystore & right host name
    if(!(Test-Path -Path "$solrRoot\bin\solr.in.cmd.old"))
    {
        Write-Host "Rewriting solr config"

        $cfg = Get-Content "$solrRoot\bin\solr.in.cmd"
        Rename-Item "$solrRoot\bin\solr.in.cmd" "$solrRoot\bin\solr.in.cmd.old"
        $newCfg = $cfg | % { $_ -replace "REM set SOLR_SSL_KEY_STORE=etc/solr-ssl.keystore.jks", "set SOLR_SSL_KEY_STORE=$certStore" }
        $newCfg = $newCfg | % { $_ -replace "REM set SOLR_SSL_KEY_STORE_PASSWORD=secret", "set SOLR_SSL_KEY_STORE_PASSWORD=secret" }
        $newCfg = $newCfg | % { $_ -replace "REM set SOLR_SSL_TRUST_STORE=etc/solr-ssl.keystore.jks", "set SOLR_SSL_TRUST_STORE=$certStore" }
        $newCfg = $newCfg | % { $_ -replace "REM set SOLR_SSL_TRUST_STORE_PASSWORD=secret", "set SOLR_SSL_TRUST_STORE_PASSWORD=secret" }
        $newCfg = $newCfg | % { $_ -replace "REM set SOLR_HOST=192.168.1.1", "set SOLR_HOST=$solrHost" }
        $newCfg | Set-Content "$solrRoot\bin\solr.in.cmd"
    }


# install the service & runs
$svc = Get-Service "$solrName" -ErrorAction SilentlyContinue
if(!($svc))
{
    Write-Host "Installing Solr service"
    &"nssm.exe" install "$solrName" "$solrRoot\bin\solr.cmd" "-f" "-p $solrPort"
    $svc = Get-Service "$solrName" -ErrorAction SilentlyContinue
}
if($svc.Status -ne "Running")
{
    Write-Host "Starting Solr service"
    Start-Service "$solrName"
}

# finally prove it's all working
$protocol = "http"
if($solrSSL -eq $true)
{
    $protocol = "https"
}
$(function () {
  $("#tree").fancytree({
    checkbox: false,
    source: fancyTreeData,
    activate: function (event, data) {
      $("#status").text("Activate: " + data.node);
    }
  });
});
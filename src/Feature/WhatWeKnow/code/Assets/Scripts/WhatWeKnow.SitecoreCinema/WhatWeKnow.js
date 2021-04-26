$(function () {
  var fancyTree = $("#tree").fancytree({
    checkbox: false,
    source: fancyTreeData,
    activate: function (event, data) {
      $("#status").text("Activate: " + data.node);
    }
  });

  var tree = $.ui.fancytree.getTree("#tree");
  tree.rootNode.children[0].setExpanded(true)
});
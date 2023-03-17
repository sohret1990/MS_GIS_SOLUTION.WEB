define([
  "dijit/_WidgetBase",
  "dojo/_base/declare"
], function (_WidgetBase, declare) {
  return declare([_WidgetBase], {
    options: {
      view: null,
      map: null,
    },
    constructor: function (options) {
      declare.safeMixin(this.options, options);
      this.set("view", this.options.view);
      this.set("map", this.options.map);
    },
    runCommand: function (pt) {
      
    },
  });
});

(function (global, undefined) {
    var demo = {};
    var lastClickedImg = null;
    var $ = $telerik.$;

    function sizePreviewWindow() {
        //because of the demo's specifics - dynamically loading an image and autosizing the RadWindow based on that
        //image's size, we need to ensure that the image is loaded before calling the autoSize() method
        demo.previewWin.autoSize(true);
    }

    function openWin(clickedImg) {
        //change the border of the clicked image
        $(clickedImg).parent().css("background-color", "red");

        //get the name of the thumbnail image
        var imgName = clickedImg.src.substring(clickedImg.src.lastIndexOf("/") + 1);
        //use the thumbnail image's name to build the src for the preview window
        demo.imgHolder.src = "Misfits/" + imgName;

        //show the window
        demo.previewWin.show();


        //clear the border of the previously clicked image
        $(lastClickedImg).parent().css("background-color", "");

        lastClickedImg = clickedImg;
    }

    function toggleExpand(clickedLink) {
        //togle the hidden pane containing extra images
        $('#hiddenPane').toggle();
        //change link's text
        if ($.trim($(clickedLink).text()) == "Show more") {
            $(clickedLink).text("Show less").addClass("showLess");
        } else {
            $(clickedLink).text("Show more").removeClass("showLess");
        }

        demo.galleryWin.restore();

        //autosize the gallery window
        demo.galleryWin.autoSize(true);
    }

    global.$autoSizeDemo = demo;
    global.sizePreviewWindow = sizePreviewWindow;
    global.toggleExpand = toggleExpand;
    global.openWin = openWin;
})(window);
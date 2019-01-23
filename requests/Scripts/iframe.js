Sys.Application.add_init(AppInit);

function AppInit() {
  var prm = Sys.WebForms.PageRequestManager.getInstance();
  prm.add_endRequest(InitializeRequest);
}
/*
function InitializeRequest(sender, args) {
  // Check to be sure this async postback is actually
  //   requesting the file download.
    if (sender._postBackSettings.sourceElement.id == "ctl00_ContentPlaceHolder1_UAbonDet1_btContract") {
    // Create an IFRAME.
    var iframe = document.createElement("iframe");

    // Get the desired region from the dropdown.
    var Commerce = $get("ctl00_ContentPlaceHolder1_UAbonDet1_hfBudjet").value;

    // Point the IFRAME to GenerateFile, with the
    //   desired region as a querystring argument.
    iframe.src = "../GetDocument.ashx?Commerce=" + Commerce;

    // This makes the IFRAME invisible to the user.
    iframe.style.display = "none";

    // Add the IFRAME to the page.  This will trigger
    //   a request to GenerateFile now.
    document.body.appendChild(iframe);
}
if (sender._postBackSettings.sourceElement.id == "ctl00_ContentPlaceHolder1_UAbonDet1_btAct") {
    // Create an IFRAME.
    var iframe = document.createElement("iframe");

    // Get the desired region from the dropdown.
    //var region = $get("Region").value;

    // Point the IFRAME to GenerateFile, with the
    //   desired region as a querystring argument.
    iframe.src = "../GetDocument.ashx?Act=Corporate";

    // This makes the IFRAME invisible to the user.
    iframe.style.display = "none";

    // Add the IFRAME to the page.  This will trigger
    //   a request to GenerateFile now.
    document.body.appendChild(iframe);
}
if (sender._postBackSettings.sourceElement.id == "ctl00_ContentPlaceHolder1_UAbonDet1_btPay" || sender._postBackSettings.sourceElement.id == "ctl00_Wizard1_btCheck") {
    // Create an IFRAME.
    var iframe = document.createElement("iframe");

    // Get the desired region from the dropdown.
    //var region = $get("Region").value;

    // Point the IFRAME to GenerateFile, with the
    //   desired region as a querystring argument.
    iframe.src = "../GetDocument.ashx";

    // This makes the IFRAME invisible to the user.
    iframe.style.display = "none";

    // Add the IFRAME to the page.  This will trigger
    //   a request to GenerateFile now.
    document.body.appendChild(iframe);
}
if (sender._postBackSettings.sourceElement.id == "ctl00_ContentPlaceHolder1_UAbonDet1_btBill") {
    // Create an IFRAME.
    var iframe = document.createElement("iframe");

    // Get the desired region from the dropdown.
    //var region = $get("Region").value;

    // Point the IFRAME to GenerateFile, with the
    //   desired region as a querystring argument.
    iframe.src = "../GetDocument.ashx?bill=1";

    // This makes the IFRAME invisible to the user.
    iframe.style.display = "none";

    // Add the IFRAME to the page.  This will trigger
    //   a request to GenerateFile now.
    document.body.appendChild(iframe);
}
if (sender._postBackSettings.sourceElement.id == "ctl00_ContentPlaceHolder1_UAbonDet1_btOrder" || sender._postBackSettings.sourceElement.id == "ctl00_Wizard1_btOrder") {
    // Create an IFRAME.
    var iframe = document.createElement("iframe");

    // Get the desired region from the dropdown.
    //var region = $get("Region").value;

    // Point the IFRAME to GenerateFile, with the
    //   desired region as a querystring argument.
    iframe.src = "../GetDocument.ashx";

    // This makes the IFRAME invisible to the user.
    iframe.style.display = "none";

    // Add the IFRAME to the page.  This will trigger
    //   a request to GenerateFile now.
    document.body.appendChild(iframe);
}
if (sender._postBackSettings.sourceElement.id == "ctl00_ContentPlaceHolder1_FAbonDet1_btAct") {
    // Create an IFRAME.
    var iframe = document.createElement("iframe");

    // Get the desired region from the dropdown.
    //var region = $get("Region").value;

    // Point the IFRAME to GenerateFile, with the
    //   desired region as a querystring argument.
    iframe.src = "../GetDocument.ashx?Act=Private";

    // This makes the IFRAME invisible to the user.
    iframe.style.display = "none";

    // Add the IFRAME to the page.  This will trigger
    //   a request to GenerateFile now.
    document.body.appendChild(iframe);
}
if (sender._postBackSettings.sourceElement.id == "ctl00_ContentPlaceHolder1_FAbonDet1_btOrder") {
    // Create an IFRAME.
    var iframe = document.createElement("iframe");

    // Get the desired region from the dropdown.
    //var region = $get("Region").value;

    // Point the IFRAME to GenerateFile, with the
    //   desired region as a querystring argument.
    iframe.src = "../GetDocument.ashx?order=1";

    // This makes the IFRAME invisible to the user.
    iframe.style.display = "none";

    // Add the IFRAME to the page.  This will trigger
    //   a request to GenerateFile now.
    document.body.appendChild(iframe);
}
if (sender._postBackSettings.sourceElement.id == "ctl00_ContentPlaceHolder1_FAbonDet1_btPay") {
    // Create an IFRAME.
    var iframe = document.createElement("iframe");

    // Get the desired region from the dropdown.
    //var region = $get("Region").value;

    // Point the IFRAME to GenerateFile, with the
    //   desired region as a querystring argument.
    iframe.src = "../GetDocument.ashx?pay=1";

    // This makes the IFRAME invisible to the user.
    iframe.style.display = "none";

    // Add the IFRAME to the page.  This will trigger
    //   a request to GenerateFile now.
    document.body.appendChild(iframe);
}
if (sender._postBackSettings.sourceElement.id == "ctl00_ContentPlaceHolder1_FAbonDet1_btOrderCheck") {
    // Create an IFRAME.
    var iframe = document.createElement("iframe");

    // Get the desired region from the dropdown.
    //var region = $get("Region").value;

    // Point the IFRAME to GenerateFile, with the
    //   desired region as a querystring argument.
    iframe.src = "../GetDocument.ashx?ordercheck=1";

    // This makes the IFRAME invisible to the user.
    iframe.style.display = "none";

    // Add the IFRAME to the page.  This will trigger
    //   a request to GenerateFile now.
    document.body.appendChild(iframe);
}
if (sender._postBackSettings.sourceElement.id == "ctl00_UAbonDet2_btAct") {
    // Create an IFRAME.
    var iframe = document.createElement("iframe");

    // Get the desired region from the dropdown.
    var act = $get("ctl00_UAbonDet2_hfClientType").value;

    // Point the IFRAME to GenerateFile, with the
    //   desired region as a querystring argument.
    iframe.src = "../GetDocument.ashx?Act=" + act;

    // This makes the IFRAME invisible to the user.
    iframe.style.display = "none";

    // Add the IFRAME to the page.  This will trigger
    //   a request to GenerateFile now.
    document.body.appendChild(iframe);
}
}
*/



//ctl00_ContentPlaceHolder1_hfClientType
//ctl00_Wizard1_btAct
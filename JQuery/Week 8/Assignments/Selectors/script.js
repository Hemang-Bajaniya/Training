$("#grocery-list").find("li:odd").css("color", "grey");

// or
// $("#grocery-list li:nth-of-type(odd)").css("color", "red");

setTimeout(() => {
    // $("#para").text("JQuery is active now");

    $("#para").html("<b>JQuery is active now</b>");
}, 1000);
document.body.childNodes.forEach(e => {
    console.log(e.nodeName, e.tagName ?? "Not exists");
});

// rewrite entire body append text
document.body.innerHTML += "<b>Footer</b>";

const articles = Array.from(document.getElementsByClassName("article"));

//data val
articles.forEach(e => {
    console.log("Data: ", e.data);
});


// only text val
articles.forEach(e => {
    console.log(e.textContent);
});

//hidden
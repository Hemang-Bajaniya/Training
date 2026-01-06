document.body.style.background = "red";

setTimeout(() => {
    document.body.style.background = "orange";
}, 1000);

// user browser, platform info
console.log(navigator.userAgent, navigator.platform);

Array.from(document.getElementsByTagName("a")).forEach(a => {
    a.addEventListener("click", confirmRedirect);
    a.has;
});

function confirmRedirect(e) {
    console.log(e.preventDefault());

    if (confirm(`Go you want to redirect?`))
        location.href = a;
}

console.log(document.documentElement, document.body, document.head);


// collecion of immediate childnodes

// Array.from(document.body.childNodes).forEach(e => {
//     console.log(e, e.parentElement);
// });

// imm siblings of node
console.log(document.body.previousSibling, document.body.nextSibling);


// elements node only, not comment, text nodes

// Array.from(document.body.children).forEach(e => {
//     console.log(e, e.parentElement);
// });

// let elem = document.getElementById("linkGroup");

// while (elem = elem.parentElement) {
//     alert(elem); // parent chain till <html>
// }


let table = document.getElementById("table");

// element with id become property of window global obj
// window.table.style.background = "red";

for (let index = 0; index < table.rows.length; index++) {
    const element = table.rows[index].cells[index];
    element.style.background = "red";
}

// querySelector
// returns elements based on matching cs selector query

let articleTitles = document.querySelectorAll(".article > h1");

articleTitles.forEach(t => {
    console.log(t);

    t.style.color = "blue";
});

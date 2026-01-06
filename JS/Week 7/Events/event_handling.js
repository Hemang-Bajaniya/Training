function handleBtnClick() {
    alert("Button has been clicked!");
}

let btn = document.getElementById("btn");
btn.onclick = handleBtnClick;

// problem
// input.onclick = function() { alert(1); }
// // ...
// input.onclick = function() { alert(2); } // replaces the previous handler

// element.addEventListener(event, handler, [options]);
// to add, remov multi evemt listner

const claimDiscount = () => {
    alert("You got 15% discount");

    btn.removeEventListener('click', handleBtnClick);
};

btn.addEventListener("click", claimDiscount, { once: true });

btn.onclick = function (event) {
    // show event type, element and coordinates of the click
    alert(event.type + " at " + event.currentTarget);
    alert("Coordinates: " + event.clientX + ":" + event.clientY);
};


class Menu {
    handleEvent(event) {
        switch (event.type) {
            case 'mousedown':
                msg.innerHTML = "Mouse button pressed";
                break;
            case 'mouseup':
                msg.innerHTML += "...and released.";
                break;
        }
    }
}

let menu = new Menu();

document.body.addEventListener('mousedown', menu);
document.body.addEventListener('mouseup', menu);

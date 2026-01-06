// init counter;
let totalCount = 0;

//grab all elements
const bulb = document.querySelector('.bulb');
const switchbtn = document.querySelector('#switch');

// append elemnt to show count dynamically
const banner = document.createElement("h3");
banner.textContent = `Total switch clicks:${totalCount}`;
document.body.append(banner);

// onclick switch handler
switchbtn.addEventListener("click", (e) => {
    if (!bulb.classList.contains('on')) {
        bulb.classList.add('on');
        switchbtn.textContent = "Turn Off";
        switchbtn.classList.add('pressed');
        document.body.style.background = "black";
        banner.style.color = "white";
    }
    else {
        bulb.classList.remove('on');
        switchbtn.textContent = "Turn On";
        switchbtn.classList.remove('pressed');
        document.body.style.background = "white";
        banner.style.color = "black";
    }

    //stop bubbling once counter reaches 5
    if (totalCount == 5) {
        e.stopPropagation();
    }
},);

// body onclick handler to show event bubbling
document.body.addEventListener("click", (e) => {
    totalCount += 1;
    banner.textContent = `Total switch clicks: ${totalCount}`;
});
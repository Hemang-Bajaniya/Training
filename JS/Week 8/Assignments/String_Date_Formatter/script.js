const timer = document.getElementById("timer");
const userId = document.getElementById("userId");
const genIdBtn = document.getElementById("genIdBtn");
const inputString = document.getElementById("inputString");
const string_frm = document.getElementById("string_frm");

class DigitalClock {
    constructor({ template }) {
        this.template = template;
    }

    show() {
        const date = new Date();
        let [h, m, s] = date.toTimeString().split(" ")[0].split(":");

        const output = this.template.replace('h', Math.round(h % 13) + 1).replace('m', m).replace('s', s);

        timer.textContent = output + " " + (h >= 13 ? "PM" : "AM");
    }

    start() {
        this.show();
        this.timer = setInterval(() => this.show(), 1000);
    }

    stop() {
        clearInterval(this.timer);
    }
}

const clock = new DigitalClock({ template: "h:m:s" });
clock.start();

string_frm.addEventListener("submit", (e) => {
    e.preventDefault();

    // console.log(inputString.value);    
    let messyString = inputString.value.trim();
    let formattedString = "";
    if (messyString.length != 0) {
        for (let word of messyString.trim().split(" ")) {
            console.log(word);
            formattedString += word[0].toUpperCase() + word.slice(1).toLowerCase() + " ";
        }

        formattedString = formattedString.trim();
    }

    // console.log(formattedString);

    inputString.value = formattedString;
});

genIdBtn.addEventListener("click", (e) => {
    const min = 1000, max = 9999;
    const randomUserId = Math.floor(Math.random() * (max - min + 1)) + min;

    userId.textContent = 'Your random user id is ' + randomUserId;
});
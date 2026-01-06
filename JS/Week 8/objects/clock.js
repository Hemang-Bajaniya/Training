class Clock {
    constructor({ template }) {
        this.template = template;
    }

    show() {
        const date = new Date();
        let [h, m, s] = date.toTimeString().split(" ")[0].split(":");

        const output = this.template.replace('h', h).replace('m', m).replace('s', s);

        console.log(output);

        return output;
    }

    start() {
        this.show();
        this.timer = setInterval(() => this.show(), 1000);
    }

    stop() {
        clearInterval(this.timer);
    }
}

class ExtendedClock extends Clock {
    constructor(options) {
        super(options);
        let { precision = 1000 } = options;
        this.precision = precision;
    }

    start() {
        this.show();
        this.timer = setInterval(() => this.show(), this.precision);
    }
};

let eclock = new ExtendedClock({ template: "h:m:s", precision: 500 });
eclock.start();

// let clock = new Clock({ template: "h:m:s" });

// clock.start();

// setTimeout(() => {
//     clock.stop();
// }, 5 * 1000);

export default Clock;
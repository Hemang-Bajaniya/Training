let animal = {
    eats: true
};
let rabbit = {
    jumps: true
};
rabbit.__proto__ = animal; // (*) 
// we can find both properties in rabbit now:
console.log(rabbit.eats); // true (**)
console.log(rabbit.jumps); // true

animal.walk = function () {
    console.log('animal is walking');
};

rabbit.walk = () => {
    console.log('rabbit is walking');
};

rabbit.walk();

console.log(Object.getOwnPropertyDescriptors(rabbit));

let longEar = {
    earLength: 10,
    __proto__: rabbit
};

// longEar -> rabbit -> animal

// walk is taken from the prototype chain 
longEar.walk(); // Animal walk
console.log(longEar.jumps); // true (from rabbit)

// TypeError: Cyclic __proto__ value
// animal.__proto__ = rabbit;

// ignored other than obj|null
animal.__proto__ = '10';

let user = {
    name: "John",
    surname: "Smith",
    set fullName(value) {
        [this.name, this.surname] = value.split(" ");
    },
    get fullName() {
        return `${this.name} ${this.surname}`;
    }
};

let admin = {
    __proto__: user,
    isAdmin: true
};

admin.fullName = "Alex Lee";

// this refer obj before .
console.log(admin.fullName, user.fullName);

// only own keys
console.log(Object.keys(longEar));

for (const key in longEar) {
    // coming from Object.proto non enumerable
    if (!Object.hasOwn(longEar, key)) {
        console.log("Inherited:", key);
        continue;
    }

    const element = longEar[key];

    console.log("Own:", key);

}

console.log(Object.getOwnPropertyDescriptors(Object.prototype));


let head = {
    glasses: 1
};

let table = {
    pen: 3,
    __proto__: head,
};

let bed = {
    sheet: 1,
    pillow: 2,
    __proto__: table,
};

let pockets = {
    money: 2000,
    __proto__: bed,
};


let start, end;

start = performance.now();
console.log(pockets.glasses);
end = performance.now();
console.log('time elapsed to access pockets.glasses using proto', end - start);


start = performance.now();
console.log(head.glasses);
end = performance.now();
console.log('time elapsed to access head.glasses directly', end - start);


let hamster = {
    stomach: [],

    eat: function (food) {
        this.stomach = [...this.stomach, food];
    }
};

function User() {
    return { "a": 1 };
}
user = new User();
console.log(User.prototype.constructor);


/* default prototype 
Rabbit.prototype = { constructor: Rabbit }; 
*/

let speedy = {
    __proto__: hamster
};

let lazy = {
    __proto__: hamster
};

speedy.eat("apple");
lazy.eat("burger");
lazy.eat("jam");
console.log(speedy.stomach); // apple 
// This one also has it, why? fix please.
console.log(lazy.stomach);




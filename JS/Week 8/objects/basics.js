let user = new Object(); // "object constructor" syntax
user = {};  // "object literal" syntax

// set properties
user.age = 24;
user.name = "Alex";
user.isAdmin = true;

//delets a property
delete user.isAdmin;

// user.isAdmin no longer avilable
// gives undefined check for null colsecing
console.log(user.isAdmin ?? false);

// multi-word prop “square bracket notation” 
user["shift time"] = "9-5";

printInfo(user);

// var can be used as key
let key = "name";
console.log(user[key]);

// not work
// console.log(user.key);

// Property value shorthand
const userLogin = (name, password) => {
    return { name, password };
};

const loggedUser = userLogin('Alex', 'password');
printInfo(loggedUser);

// check exsistance

// by check undefined
console.log(loggedUser.loginTime === undefined);

// incorrect when prop = undefined exists
loggedUser.password = undefined;
console.log(loggedUser.password == undefined);

// use in op
console.log("password" in loggedUser);

// ordering prop
let codes = {
    "49": "Germany",
    "41": "Switzerland",
    "44": "Great Britain",
    "1": "USA",
    'two': 2,
    'one': 1
};

printInfo(codes);

// Copying by reference

let newUser = loggedUser;
newUser.password = "newpassword";

printInfo(loggedUser);

let o1 = {}, o2 = {};
console.log(o1 == o2, newUser === loggedUser);

// Const object
const logger = {
    location: "/files/logs",
    fullInfo: true,
};

// new prop can be added, modify
logger.ignore = ['./dummy.html', './new.jsx'];
logger.fullInfo = false;

// logger = {};

printInfo(logger);


// cloning obj
user = { name: "John" };
let permissions1 = { canView: true };
let permissions2 = { canEdit: true };

Object.assign(user, permissions1, permissions2);
// now user = { name: "John", canView: true, canEdit: true }

user = { name: "John" };
// overwrite name, add isAdmin 
Object.assign(user, { name: "Pete", isAdmin: true });
// now user = { name: "Pete", isAdmin: true }

const shirt = {
    id: 101,
    category: "clothware",
    dim: {
        length: 6,
        width: 3
    }
};

// when prop are ref types
// all below will create a shallow copy

// const pant = Object.assign({}, shirt);
// for (const key in shirt) {
//     if (!Object.hasOwn(shirt, key)) continue;

//     const element = shirt[key];
//     pant[key] = element;
// }


// create a deep copy
const pant = structuredClone(shirt);

// noe pointing to diff ref, but...
console.log(pant == shirt, pant === shirt);

// values changed
pant.id = 102;
pant.dim.length = 7;
pant.dim.width = 2.7;

printInfo([shirt, pant]);

// this
loggedUser.logout = function () {
    console.log("clear cookis");
};

loggedUser.logout();

console.log(globalThis);

//  Constructor function
function User(name) {
    // this = {};  (implicitly) 
    // add properties to this 
    this.name = name;
    this.isAdmin = false;
    //
}
let user1 = new User("Jack");
console.log
    (user1.name); // Jack
console.log
    (user1.isAdmin); // false




// let salaries = {
//     John: 100,
//     Ann: 160,
//     Pete: 130
// };

// let total = 0;
// for (const key in salaries) {
//     if (!Object.hasOwn(salaries, key)) continue;

//     const element = Number.parseFloat(salaries[key]) ?? 0;
//     total += element;
// }
// console.log(total);

// let menu = {
//     width: 200,
//     height: 300,
//     title: "My menu"
// };

// multiplyNumeric(menu);

// function multiplyNumeric(object) {
//     for (const key in object) {
//         if (!Object.hasOwn(object, key)) continue;

//         const element = object[key];
//         if (typeof (element) == 'number') {
//             object[key] *= 2;
//         }


//     }
// }

// printInfo(menu);






// helper fn
function printInfo(obj) {
    console.table(obj);
};


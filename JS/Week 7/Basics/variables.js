"use strict"; // froce to use modern ver

let x = 10, y = 20, z = 30; // multi assign
console.log(x, y, z);

// let x = 100; block scoped var

let $ = 20, _ = 10; // can start with $, _
console.log($, _);

// let 1kd = 1020; 

// data = "Secret code"; // not supported in modern syntax
// console.log(data);

const OK = 202, ERROR = 404, SERVER_ERROR = 500; // uppcase const var for hardcoded val

// OK = 201; // TypeError: Assignment to constant variable.

const loadTime = await performance.timeOrigin; // camleCase for exe time const val dont know

console.log(OK, loadTime);

let ourPlanetName = "Earth", currentUserName = "Alex";
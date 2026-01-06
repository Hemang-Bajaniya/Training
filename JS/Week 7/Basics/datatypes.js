// 8 basic dtype

let x = 10, y = "Hello", z = true, obj = { "Name": "Alex" };
let arr = []; // arr is obj too

console.log(typeof (x), typeof (y), typeof (z), typeof (obj), typeof (arr));

// Number
let n = 123;
n = 12.345;

// special no
let inf = 1 / 0, minInf = -1 / 0, nan = 1 + false;
console.log(inf == Infinity, minInf, nan);
console.log(typeof (inf), typeof (minInf), typeof (nan)); // 

console.log(Infinity);

console.log(NaN ** 0, NaN + 0);



// Big int
console.log(2 ** 53 + 2);
console.log(typeof (2 ** 53 + 1));

const bigInt = 2n ** 100n; // arbitary len val
console.log(bigInt, typeof (bigInt));



// string
let name = "abc";
console.log(name);


// bool
console.log(10 > 20);

// null
const eventHandler = null;
console.log("Hello");

// undefined
// The meaning of undefined is “value is not assigned”.
let val;
console.log(val);

val = null;
console.log(null ?? "default val"); // null safety

console.log(typeof null);





for (let i = 1; i <= 5; i++) {
    console.log("Number:", i);
}

let count = 1;

while (count <= 5) {
    console.log("Count:", count);
    count++;
}

let num = 1;

do {
    console.log("Num:", num);
    num++;
} while (num <= 5);

const colors = ["red", "green", "blue"];

for (const color of colors) {
    console.log(color);
}

const user = {
    name: "Alice",
    age: 22,
    city: "Paris"
};

for (const key in user) {
    console.log(key + ":", user[key]);
}

let matrix = [[10, 20, 30], [0, -9, -1, 0]];
console.log(typeof matrix);

loop: for (let i = 0; i < matrix.length; i++) {
    for (let j = 0; j < matrix[i].length; j++) {
        if (matrix[i][j] == -1)
            break loop; // break the label loop stat

        console.log(matrix[i][j]);

    }
    console.log();

}

let a = 2 + 2;

a = "3";

switch (a) {
    case 3: // strict === check;
        console.log('Too small');
        break;
    case 4:
        console.log('Exactly!');
        break;
    case 5:
        console.log('Too big');
        break;
    default:
        console.log("I don't know such values");
}
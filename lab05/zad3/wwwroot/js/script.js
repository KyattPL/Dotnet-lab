function createHeader(howMany, nums) {

    const theadObj = document.getElementById('tabhead');

    theadObj.appendChild(document.createElement('th'));

    for (let x = howMany; x > 0; x--) {
        let temp = document.createElement('th');
        temp.innerText = nums[howMany - x];
        theadObj.appendChild(temp);
    }
}


function createRows(howMany, nums) {
    const tbodyObj = document.getElementById('tabbody');

    for (let x = howMany; x > 0; x--) {

        let newRow = document.createElement('tr');
        tbodyObj.appendChild(newRow);

        let header = document.createElement('th');
        header.innerText = nums[howMany - x];
        newRow.appendChild(header);

        for (let y = howMany; y > 0; y--) {
            let value = nums[howMany - x] * nums[howMany - y];
            let node = document.createElement('td');
            if (value % 2 == 0) {
                node.className = 'even';
            } else {
                node.className = 'odd';
            }
            node.innerText = value;
            newRow.appendChild(node);
        }
    }
}

function getSize() {
    const howMany = prompt("Podaj rozmiar tabelki (5-20): ", 10);
    if (isNaN(howMany) || howMany < 5 || howMany > 20) {
        let errorP = document.createElement('p');
        errorP.innerText = "Podane dane były niewłaściwe, więc przyjęto n=10";
        document.getElementById('errDiv').appendChild(errorP);
        return 10;
    } else {
        return howMany;
    }
}

function createTable() {
    const howMany = getSize();

    const randomNums = [];

    for (let x = howMany; x > 0; x--) {
        randomNums.push(Math.floor(Math.random() * 98) + 1);
    }

    createHeader(howMany, randomNums);
    createRows(howMany, randomNums);
}

createTable();

const canvas = document.getElementById('canv');
const ctx = canvas.getContext('2d');

const mouse = {
    x: 200,
    y: 100,
};

canvas.onmousemove = (e) => {
    let rect = canvas.getBoundingClientRect();
    mouse.x = e.clientX - rect.left;
    mouse.y = e.clientY - rect.top;

    ctx.clearRect(0, 0, canvas.width, canvas.height);

    ctx.lineWidth = 5;
    ctx.strokeStyle = 'rgba(230, 105, 3, 0.5)';
    ctx.strokeRect(0, 0, canvas.width, canvas.height);

    ctx.strokeStyle = 'rgba(0,0,170, 0.5)';

    ctx.beginPath();
    ctx.moveTo(0, 0);
    ctx.lineTo(mouse.x, mouse.y);
    ctx.stroke();

    ctx.beginPath();
    ctx.moveTo(0, canvas.height);
    ctx.lineTo(mouse.x, mouse.y);
    ctx.stroke();

    ctx.beginPath();
    ctx.moveTo(canvas.width, 0);
    ctx.lineTo(mouse.x, mouse.y);
    ctx.stroke();

    ctx.beginPath();
    ctx.moveTo(canvas.width, canvas.height);
    ctx.lineTo(mouse.x, mouse.y);
    ctx.stroke();
}

canvas.onmouseleave = (e) => {
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    ctx.lineWidth = 5;
    ctx.strokeStyle = 'rgba(230, 105, 3, 0.5)';
    ctx.strokeRect(0, 0, canvas.width, canvas.height);

    ctx.strokeStyle = 'rgba(0,0,170, 0.5)';
}

canvas.on = (e) => {
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    ctx.lineWidth = 5;
    ctx.strokeStyle = 'rgba(230, 105, 3, 0.5)';
    ctx.strokeRect(0, 0, canvas.width, canvas.height);

    ctx.strokeStyle = 'rgba(0,0,170, 0.5)';
}

ctx.lineWidth = 5;
ctx.strokeStyle = 'rgba(230, 105, 3, 0.5)';
ctx.strokeRect(0, 0, canvas.width, canvas.height);

ctx.strokeStyle = 'rgba(0,0,170, 0.5)';
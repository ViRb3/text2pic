var values = []; //[7, 8, 9];
// var numOfImages = [];
var image = []; //[["x", "x", "x"], ["y", "y", "y"], ["y", "y", "y"], ["y", "y", "y"], ["y", "y", "y"]];

function createBlock(word) {
    var body = document.getElementsByTagName('body');
    var tbl = document.createElement('table');
    tbl.setAttribute('border', 1);
    var tbdy = document.createElement('tbody');

    var tr1 = document.createElement('tr');
    for (var i = 0; i < image.length; i++) {
        var td1 = document.createElement('td');
        var button = document.createElement('button');
        button.setAttribute('onclick', 'next_image_up(this.id)');
        button.setAttribute('id', i.toString());
        button.appendChild(document.createTextNode("Next"));
        td1.appendChild(button);
        tr1.appendChild(td1);
    }
    tbdy.appendChild(tr1);
    var tr2 = document.createElement('tr');
    for (var i = 0; i < image.length; i++) {
        var td2 = document.createElement('td');
        var img = (document.createElement("img"));
        img.setAttribute('id', i.toString() + "image");
        img.setAttribute('src', image[i][0]);
        img.setAttribute('width', '200px');
        td2.appendChild(img);
        tr2.appendChild(td2);
    }
    tbdy.appendChild(tr2);
    var tr3 = document.createElement('tr');
    for (var i = 0; i < image.length; i++) {
        var td3 = document.createElement('td');
        td3.appendChild(document.createTextNode(word[i]));
        tr3.appendChild(td3);
    }
    tbdy.appendChild(tr3);
    var tr4 = document.createElement('tr');
    for (var i = 0; i < image.length; i++) {
        var td4 = document.createElement('td');
        var button = document.createElement('button');
        button.setAttribute('onclick', 'next_image_down(this.id)');
        button.setAttribute('id', i.toString());
        button.appendChild(document.createTextNode("Prev"));
        td4.appendChild(button);
        tr4.appendChild(td4);
    }
    tbdy.appendChild(tr4);
    tbl.appendChild(tbdy);
    document.body.appendChild(tbl);
}

function getVal(data, status) {
    var words = [];
    var i = [];
    var obj = JSON.parse(data);
    for (var key in obj) {
        words.push(key);
        console.log(key);
        for (var k in obj[key]) {
            console.log(obj[key][k]);
            i.push(obj[key][k]);
            
        }
        image.push(i);
        i = [];
    }


    createBlock(words);
}

function getImages() {
    alert("im running");

    var s = document.getElementById("inp").value;
    $.get("http://localhost:5000/API/Text2Pic/" + s, getVal);
    //calls the method

    //populate values

}

function next_image_up(clicked_id) {
    var x = parseInt(clicked_id);
    if (values[x] == 9) {
        values[x] = 0;
    } else {
        values[x]++;
    }
    alert(values[x])
}


function next_image_down(clicked_id) {
    var x = parseInt(clicked_id);
    if (values[x] == 0) {
        values[x] = 9;
    } else {
        values[x]--;
    }

}
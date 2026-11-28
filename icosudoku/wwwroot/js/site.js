// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function addico(svg) {
    var newElement = document.createElementNS("http://www.w3.org/2000/svg", 'path'); //Create a path in SVG's namespace
    newElement.setAttribute("d", "M 100 120 L 150 50 L 200 120 L 250 50 L 300 120 L 350 50 L 400 120 L 450 50 L 500 120 L 550 50 L 600 120 L 100 120 L 50 190 L 100 120 L 150 190 L 200 120 L 250 190 L 300 120 L 350 190 L 400 120 L 450 190 L 500 120 L 550 190 L 600 120 L 550 190 L 50 190 L 100 260 L 150 190 L 200 260 L 250 190 L 300 260 L 350 190 L 400 260 L 450 190 L 500 260 L 550 190"); //Set path's data
    newElement.setAttribute("fill", "transparent");
    newElement.setAttribute("stroke", "black");
    svg.appendChild(newElement);

}

function addvertex(svg, id, val, x, y, w, h) {
    var newElement = document.createElementNS("http://www.w3.org/2000/svg", 'foreignObject'); //Create a path in SVG's namespace
    newElement.setAttribute("x", x);
    newElement.setAttribute("y", y);
    newElement.setAttribute("width", w);
    newElement.setAttribute("height", h);
    var innerhtml = "<div class=smalltext><select onchange='vertexonchange(this.id, this.value);' id='" + id + "' name='" + id + "'>";
    for (i = 0; i < 13; i++) {
        innerhtml += "<option value='" + i + "'";
        if (i == val) {
            innerhtml += " selected";
        }
        innerhtml += ">" + i + "</option>"
    }
    innerhtml += "</select></div>";
    newElement.innerHTML = innerhtml;
    svg.appendChild(newElement);
    
}

function addvertexlabel(svg, id, val, x, y, w, h) {
    var newElement = document.createElementNS("http://www.w3.org/2000/svg", 'foreignObject'); //Create a path in SVG's namespace
    newElement.setAttribute("x", x);
    newElement.setAttribute("y", y);
    newElement.setAttribute("width", w);
    newElement.setAttribute("height", h);
    newElement.innerHTML = "<div class=smalltext id='" + id + "' name='" + id +"'>"+val+"</div>";
    svg.appendChild(newElement);
    return newElement;
}

function addtvertexlabel(svg, tn, v, val, x, y, w, h) {
    var newElement = document.createElementNS("http://www.w3.org/2000/svg", 'foreignObject'); //Create a path in SVG's namespace
    newElement.setAttribute("x", x);
    newElement.setAttribute("y", y);
    newElement.setAttribute("width", w);
    newElement.setAttribute("height", h);
    newElement.innerHTML = "<div class=smalltext id='t[" + tn + "]." + v + "' name='t[" + tn + "]." + v +"'>"+val+"</div>";
    svg.appendChild(newElement);
    return newElement;
}

function vertexonchange(id, value) {
    const elements = document.querySelectorAll("[id^='label"+id+"']");
    elements.forEach(element => element.innerHTML = value);
}

function draw_sol(id, v1, v2, v3) {
    const elements = document.querySelectorAll("[id^='t\[" + id + "\]\.']");
    elements[0].innerHTML = v1;
    elements[1].innerHTML = v2;
    elements[2].innerHTML = v3;
}

function draw_gui(svg,a,b,c,d,e,f,g,h,i,j,k,l,sol) {
    //var svg = document.getElementsByTagName('svg')[0]; //Get svg element
    addico(svg);
    

    addtvertexlabel(svg, 0, "v1", "*", 120, 65, 60, 30);
    addtvertexlabel(svg, 0, "v2", "*", 95, 95, 60, 30);
    addtvertexlabel(svg, 0, "v3", "*", 145, 95, 60, 30);

    addtvertexlabel(svg, 1, "v1", "*", 220, 65, 60, 30);
    addtvertexlabel(svg, 1, "v2", "*", 195, 95, 60, 30);
    addtvertexlabel(svg, 1, "v3", "*", 245, 95, 60, 30);

    addtvertexlabel(svg, 2, "v1", "*", 320, 65, 60, 30);
    addtvertexlabel(svg, 2, "v2", "*", 295, 95, 60, 30);
    addtvertexlabel(svg, 2, "v3", "*", 345, 95, 60, 30);

    addtvertexlabel(svg, 3, "v1", "*", 420, 65, 60, 30);
    addtvertexlabel(svg, 3, "v2", "*", 395, 95, 60, 30);
    addtvertexlabel(svg, 3, "v3", "*", 445, 95, 60, 30);

    addtvertexlabel(svg, 4, "v1", "*", 520, 65, 60, 30);
    addtvertexlabel(svg, 4, "v2", "*", 495, 95, 60, 30);
    addtvertexlabel(svg, 4, "v3", "*", 545, 95, 60, 30);

    addtvertexlabel(svg, 5, "v1", "*", 70, 135, 60, 30);
    addtvertexlabel(svg, 5, "v2", "*", 45, 165, 60, 30);
    addtvertexlabel(svg, 5, "v3", "*", 95, 165, 60, 30);

    addtvertexlabel(svg, 6, "v1", "*", 95, 135, 60, 30);
    addtvertexlabel(svg, 6, "v2", "*", 120, 165, 60, 30);
    addtvertexlabel(svg, 6, "v3", "*", 145, 135, 60, 30);

    addtvertexlabel(svg, 7, "v1", "*", 170, 135, 60, 30);
    addtvertexlabel(svg, 7, "v2", "*", 145, 165, 60, 30);
    addtvertexlabel(svg, 7, "v3", "*", 195, 165, 60, 30);

    addtvertexlabel(svg, 8, "v1", "*", 195, 135, 60, 30);
    addtvertexlabel(svg, 8, "v2", "*", 220, 165, 60, 30);
    addtvertexlabel(svg, 8, "v3", "*", 245, 135, 60, 30);

    addtvertexlabel(svg, 9, "v1", "*", 270, 135, 60, 30);
    addtvertexlabel(svg, 9, "v2", "*", 245, 165, 60, 30);
    addtvertexlabel(svg, 9, "v3", "*", 295, 165, 60, 30);

    addtvertexlabel(svg, 10, "v1", "*", 295, 135, 60, 30);
    addtvertexlabel(svg, 10, "v2", "*", 320, 165, 60, 30);
    addtvertexlabel(svg, 10, "v3", "*", 345, 135, 60, 30);

    addtvertexlabel(svg, 11, "v1", "*", 370, 135, 60, 30);
    addtvertexlabel(svg, 11, "v2", "*", 345, 165, 60, 30);
    addtvertexlabel(svg, 11, "v3", "*", 395, 165, 60, 30);

    addtvertexlabel(svg, 12, "v1", "*", 395, 135, 60, 30);
    addtvertexlabel(svg, 12, "v2", "*", 420, 165, 60, 30);
    addtvertexlabel(svg, 12, "v3", "*", 445, 135, 60, 30);

    addtvertexlabel(svg, 13, "v1", "*", 470, 135, 60, 30);
    addtvertexlabel(svg, 13, "v2", "*", 445, 165, 60, 30);
    addtvertexlabel(svg, 13, "v3", "*", 495, 165, 60, 30);

    addtvertexlabel(svg, 14, "v1", "*", 495, 135, 60, 30);
    addtvertexlabel(svg, 14, "v2", "*", 520, 165, 60, 30);
    addtvertexlabel(svg, 14, "v3", "*", 545, 135, 60, 30);

    addtvertexlabel(svg, 15, "v1", "*", 45, 205, 60, 30);
    addtvertexlabel(svg, 15, "v2", "*", 70, 235, 60, 30);
    addtvertexlabel(svg, 15, "v3", "*", 95, 205, 60, 30);

    addtvertexlabel(svg, 16, "v1", "*", 145, 205, 60, 30);
    addtvertexlabel(svg, 16, "v2", "*", 170, 235, 60, 30);
    addtvertexlabel(svg, 16, "v3", "*", 195, 205, 60, 30);

    addtvertexlabel(svg, 17, "v1", "*", 245, 205, 60, 30);
    addtvertexlabel(svg, 17, "v2", "*", 270, 235, 60, 30);
    addtvertexlabel(svg, 17, "v3", "*", 295, 205, 60, 30);

    addtvertexlabel(svg, 18, "v1", "*", 345, 205, 60, 30);
    addtvertexlabel(svg, 18, "v2", "*", 370, 235, 60, 30);
    addtvertexlabel(svg, 18, "v3", "*", 395, 205, 60, 30);

    addtvertexlabel(svg, 19, "v1", "*", 445, 205, 60, 30);
    addtvertexlabel(svg, 19, "v2", "*", 470, 235, 60, 30);
    addtvertexlabel(svg, 19, "v3", "*", 495, 205, 60, 30);

    addvertex(svg, "A", a, 120, 40, 60, 30);
    addvertexlabel(svg, "labelA1", a, 220, 35, 60, 30);
    addvertexlabel(svg, "labelA2", a, 320, 35, 60, 30);
    addvertexlabel(svg, "labelA3", a, 420, 35, 60, 30);
    addvertexlabel(svg, "labelA4", a, 520, 35, 60, 30);
    addvertex(svg, "B", b, 70, 110, 60, 30);
    addvertex(svg, "C", c, 170, 110, 60, 30);
    addvertex(svg, "D", d, 270, 110, 60, 30);
    addvertex(svg, "E", e, 370, 110, 60, 30);
    addvertex(svg, "F", f, 470, 110, 60, 30);
    addvertexlabel(svg, "labelB1", b, 580, 110, 60, 30);
    addvertex(svg, "G", g, 20, 180, 60, 30);
    addvertex(svg, "H", h, 120, 180, 60, 30);
    addvertex(svg, "I", i, 220, 180, 60, 30);
    addvertex(svg, "J", j, 320, 180, 60, 30);
    addvertex(svg, "K", k, 420, 180, 60, 30);
    addvertexlabel(svg, "labelG1", g, 530, 180, 60, 30);
    addvertex(svg, "L", l, 70, 250, 60, 30);
    addvertexlabel(svg, "labelL1", l, 170, 260, 60, 30);
    addvertexlabel(svg, "labelL2", l, 270, 260, 60, 30);
    addvertexlabel(svg, "labelL3", l, 370, 260, 60, 30);
    addvertexlabel(svg, "labelL4", l, 470, 260, 60, 30);
}



/*show sweetalert error*/
function alertError(message) {
    Swal.fire({
        icon: 'error',
        text: message,
    })
}

/*show sweetalert success*/
function alertSuccess(message) {
    Swal.fire({
        icon: 'success',
        text: message,
    })
}

function htmlFunction() {


    document.getElementsByClassName('title')[0].innerHTML = 'The Basic Languange of the web : HTML';

    document.getElementsByClassName('date-post')[0].innerHTML = 'Posted by <b>Mozilla</b> on Monday, June 21st 2027';
    document.getElementById('image').src = '/image/pic_html.jpeg';

    document.getElementsByClassName('body-topic')[0].innerHTML = 'HTML (HyperText Markup Language) is the most basic building block of the Web. It defines the meaning and structure of web content. Other technologies besides HTML are generally used to describe a web page appearance/presentation (CSS) or functionality / behavior(JavaScript).';
}

function cssFunction() {

    document.getElementsByClassName('title')[0].innerHTML = 'The Basic Languange of the web : CSS';

    document.getElementsByClassName('date-post')[0].innerHTML = 'Posted by <b>Mozilla</b> on Saturday, June 24st 2027';
    document.getElementById('image').src = '/image/pic_css.png';

    document.getElementsByClassName('body-topic')[0].innerHTML = 'Cascading Style Sheets (CSS) is a stylesheet language used to describe the presentation of a document written in HTML or XML (including XML dialects such as SVG, MathML or XHTML). CSS describes how elements should be rendered on screen, on paper, in speech, or on other media.';
}


function javascriptFunction() {


    document.getElementsByClassName('title')[0].innerHTML = 'The Basic Languange of the web : JavaScript';

    document.getElementsByClassName('date-post')[0].innerHTML = 'Posted by <b>Mozilla</b> on Sunday, June 28st 2027';
    document.getElementById('image').src = '/image/pic_javascript.jpg';

    document.querySelector('.body-topic').innerHTML = 'JavaScript (JS) is a lightweight, interpreted, or just-in-time compiled programming language with first-class functions. While it is most well-known as the scripting language for Web pages, many non-browser environments also use it. ';

}

function animalFunction() {

    /*data animals*/
    let animals = [
        { name: 'bimo', species: 'cat', kelas: { name: "mamalia" } },
        { name: 'budi', species: 'cat', kelas: { name: "mamalia" } },
        { name: 'nemo', species: 'snail', kelas: { name: "invertebrata" } },
        { name: 'dori', species: 'cat', kelas: { name: "mamalia" } },
        { name: 'simba', species: 'snail', kelas: { name: "invertebrata" } }
    ]


    /*new array use store data only cat*/
    let onlyCat = [];

    /*add data species cat in onlyCat*/
    for (var i = 0; i < animals.length; i++) {
        if (animals[i].species == "cat") {
            onlyCat.push(animals[i]);
        }
    }
    console.log(onlyCat);

    /*replace class name non-mamalia if species snail */
    for (var i = 0; i < animals.length; i++) {
        if (animals[i].species == "snail") {
            animals[i].kelas.name = "Non-Mamalia"
        }
    }
    console.log(animals);


    let arr = ['Apple', { name: 'John' }, true, function () { alert('Hello'); }];
    console.log(arr[1].name);
}
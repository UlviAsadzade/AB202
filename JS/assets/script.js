// let box = document.querySelector(".box");

// const Cars = [
//     {
//         Name:"Mustang",
//         Price:120564,
//         Image: "https://i.ytimg.com/vi/PqW8awlbT5g/maxresdefault.jpg"
//     },
//     {
//         Name:"Zapi",
//         Price:120,
//         Image: "https://i.ytimg.com/vi/PqW8awlbT5g/maxresdefault.jpg"
//     },
//     {
//         Name:"BMW",
//         Price:320,
//         Image: "https://i.ytimg.com/vi/PqW8awlbT5g/maxresdefault.jpg"
//     }

// ]

// for (let i = 0; i < Cars.length; i++) {
//     box.innerHTML +=`
//     <div class="card col-lg-4 col-md-6" >
//     <img src="${Cars[i].Image}" class="card-img-top" alt="...">
//     <div class="card-body">
//       <h5 class="card-title">Name: ${Cars[i].Name}</h5>
//       <p class="card-text">Price: ${Cars[i].Price}</p>
//       <a href="#" class="btn btn-primary">Go somewhere</a>
//     </div>
//   </div>
//     `
    
// }



let ol = document.getElementById("box");

let form = document.getElementById("form");

let nameinput =  document.getElementById("name");
let surnameinput =  document.getElementById("surname");
let button = document.getElementById("submitbtn")

// button.addEventListener("click",function(e){
//     e.preventDefault();
//     let li = document.createElement("li");
//     if(nameinput.value=="" || surnameinput.value==""){
//         alert("Bosdur")
//     }
//     else{
//         li.innerHTML= nameinput.value + " "+ surnameinput.value
//         ol.appendChild(li);
//         nameinput.value=""
//         surnameinput.value=""
//     }
   
// })



nameinput.addEventListener("keyup",function(e){
    e.preventDefault()
    console.log(e.target.value);
})
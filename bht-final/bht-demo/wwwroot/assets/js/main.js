$(document).ready(function () {

    // Search Input Partial
    $(document).on("keyup", "#search-input", function () {
        let inputVal = $(this).val().trim();
        $("#search-list .row").remove();
        $.ajax({
            method: "get",
            url: "https://localhost:44388/tour/search?search=" + inputVal,
            success: function (res) {
                $("#search-list").append(res);
            }
        })
    })




  const cont = document.querySelector("header").firstElementChild;
  function lgFunction(lgWidth) {
    if (lgWidth.matches) {
      cont.className = "container-fluid";
    } else {
      cont.className = "container";
    }
  }
  let lgWidth = window.matchMedia("(max-width: 991.98px)");
  lgFunction(lgWidth);
  lgWidth.addListener(lgFunction);

  const contF = document.querySelector("header").querySelector(".dropdown-menu").firstElementChild;
  function smFunction(smWidth) {
    if (smWidth.matches) {
      contF.className = "container-fluid";
    } else {
      contF.className = "container";
    }
  }
  let smWidth = window.matchMedia("(max-width: 575.98px)");
  smFunction(smWidth);
  smWidth.addListener(smFunction);

  const dropdownMenu = document.querySelector("header").querySelector(".dropdown-menu");
  const openMenu = document.querySelector("header").querySelector(".menu-icon").querySelector("img");
  const closeMenu = document.querySelector("header").querySelector(".close-menu-icon");
  openMenu.addEventListener("click", function(){
    $(dropdownMenu).slideToggle();
  })
  closeMenu.addEventListener("click", function(){
    $(dropdownMenu).slideToggle();
  })

  $(".phone").hover(function () {
    $(".phone-dropdown").stop(true, false, true).slideToggle(400);
  });

  const seacrh = document.querySelector(".search");
  const searchBox = document.querySelector(".search-box");
  const menu = document.querySelector(".menu").querySelector("ul");
  seacrh.addEventListener("click", function () {
    let searchBoxOpacity = searchBox.style.opacity;
    if (searchBoxOpacity == 0) {
      searchBox.style.opacity = "1";
      searchBox.style.pointerEvents = "auto";
      menu.style.opacity = "0";
    }
    if (searchBoxOpacity == 1) {
      searchBox.style.opacity = "0";
      searchBox.style.pointerEvents = "none";
      menu.style.opacity = "1";
    }
  });

  function mdFunction(mdWidth) {
    const tours = document.querySelectorAll(".carousel-tours");
    tours.forEach((item) => {
      let tourGroup = item.firstElementChild.lastElementChild;
      let placeOwl = document.querySelector("#places").querySelector(".place-ul");
      if (mdWidth.matches) {
        tourGroup.classList.remove(
          "row",
          "row-cols-lg-4",
          "row-cols-md-2",
          "g-3"
        );
        placeOwl.classList.remove(
          "row",
          "row-cols-lg-3",
          "row-cols-md-2"
        )
        tourGroup.classList.add("owl-carousel", "owl-theme", "owl-tours");
        placeOwl.classList.add("owl-carousel", "owl-theme", "owl-place");
        $(".owl-tours").owlCarousel({
          loop: true,
          margin: 10,
          nav: false,
          responsive: {
            0: {
              items: 1,
            },
          },
        });
        $(".owl-place").owlCarousel({
          loop: true,
          margin: 10,
          nav: false,
          dots:true,
          responsive: {
            0: {
              items: 1,
            },
          },
        });
      }
    });
  }
  let mdWidth = window.matchMedia("(max-width: 767.98px)");
  mdFunction(mdWidth);
  mdWidth.addListener(mdFunction);

  // Vacancy js
  const vacancyEntry = document.querySelectorAll(
    ".vacancy-item .vacancy-entry"
  );
  const vacancyInner = document.querySelector(".vacancy-inner");

  vacancyEntry.forEach((el) => {
    el.addEventListener("click", function () {
      $(vacancyInner).slideToggle();
    });
  });

  //--------------- Swiper js code -----------------------
  var swiper = new Swiper(".mySwiper", {
    navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev",
    },
  });

  //------------ Weather API --------------

  let ctryTemp = document.querySelectorAll(".ctry-temp");
  ctryTemp.forEach((item) => {
    let ctryLat = item.firstElementChild.textContent;
    let ctryLon = item.lastElementChild.textContent;
    const url = `https://api.openweathermap.org/data/2.5/weather?lat=${ctryLat}&lon=${ctryLon}&appid=68e19672430fb4786ee026ac59a16f52&units=metric`;
    // Make a request for a user with a given ID
    axios
      .get(url)
      .then(function (response) {
        item.innerHTML = `${Math.round(response.data.main.temp)}°`;
      })
      .catch(function (error) {
        console.log(error);
      });
  });

  //------------- Owl Carousel ---------------
  // $('.owl-nav').firstElementChild.innerHTML = `<img src="./assets/icon/arrow-left.png" alt="">`;
  // console.log($('.owl-nav'));
  $(".owl-featured").owlCarousel({
    loop: true,
    margin: 10,
    nav: true,
    responsive: {
      0: {
        items: 1,
        nav: false,
      },
      600: {
        items: 2,
        nav: false,
      },
      1000: {
        items: 3,
      },
    },
  });
  $(".owl-prev span").html('<img src="./assets/icon/arrow-left.png" alt="">');
  $(".owl-next span").html('<img src="./assets/icon/arrow-right.png" alt="">');

  // gallery page js
  const galleryBtns = document
    .querySelector(".gallery-list")
    .querySelectorAll("a");
  const gallerBtn1 = galleryBtns[0];
  const gallerBtn2 = galleryBtns[1];
  gallerBtn1.addEventListener("click", function () {
    this.classList.add("active");
    gallerBtn2.classList.remove("active");
  });
  gallerBtn2.addEventListener("click", function () {
    this.classList.add("active");
    gallerBtn1.classList.remove("active");
  });


    
});

// tour category
const tourInnerItem = document.querySelectorAll(".tour-inner-item");
const cateTitle = document.querySelector(".cate-title");
tourInnerItem.forEach(item => {
    item.addEventListener("click", function () {
        cateTitle.innerHTML = `
    <img src="./assets/icon/menu.png" class="c-icon" alt="" />
    ${item.textContent}
    `;
    })
});

$(".tour-menu-list").hover(function () {
    $(".tour-inner-list").stop(true, false, true).slideToggle(400);
});

// Tour Detail----------

const tourDetailText = document
  .querySelector(".tour-info-desc")
  .querySelector(".text");
const tourDescBtn = document.querySelector(".tour-desc-btn");
const tourBtn = document
  .querySelector(".btn-list")
  .querySelectorAll(".btn-item");
const gallery = document.getElementById("tour").querySelector(".gallery");
const tourVideo = document.getElementById("tour").querySelector(".tour-video");
const tourMap = document.getElementById("tour").querySelector(".tour-map");

tourDescBtn.addEventListener("click", function () {
  tourDetailText.style.height = "auto";
  tourDescBtn.style.display = "none";
});

tourBtn.forEach((btn) => {
  btn.addEventListener("click", function () {
    if (tourBtn[0] == btn) {
      gallery.style.display = "block";
      tourVideo.style.display = "none";
      tourMap.style.display = "none";
    }
    if (tourBtn[1] == btn) {
      gallery.style.display = "none";
      tourVideo.style.display = "block";
      tourMap.style.display = "none";
    }
    if (tourBtn[2] == btn) {
      gallery.style.display = "none";
      tourVideo.style.display = "none";
      tourMap.style.display = "block";
    }
  });
});

// ------- Gallery Slider --------------
let slideIndex = 1;
showSlides(slideIndex);

function plusSlides(n) {
  showSlides((slideIndex += n));
}

function currentSlide(n) {
  showSlides((slideIndex = n));
}

function showSlides(n) {
  let i;
  let slides = document.getElementsByClassName("mySlides");
  let dots = document.getElementsByClassName("demo");
  if (n > slides.length) {
    slideIndex = 1;
  }
  if (n < 1) {
    slideIndex = slides.length;
  }
  for (i = 0; i < slides.length; i++) {
    slides[i].style.display = "none";
  }
  for (i = 0; i < dots.length; i++) {
    dots[i].className = dots[i].className.replace(" active", "");
  }
  slides[slideIndex - 1].style.display = "block";
  dots[slideIndex - 1].className += " active";
  // captionText.innerHTML = dots[slideIndex-1].alt;
}

// Pre Loader
$(window).on("load", function(e) {
    // Animate loader off screen
    setInterval(function() {
        $(".catx-preloader").fadeOut("slow");
    }, 2000);
})
$(document).ready(function() {
    $('.animate-login-form').delay(1000).fadeIn();
    /*$('.special-login-animate video').bind('ended', function(){
        $(this).parent().fadeOut("slow");
    });*/
    // Form
    $('input').focus(function() {
        $(this).addClass("input-focus").parent().addClass("focus-active").find('label').addClass("to-top");
    })
    $('input').blur(function() {
        if($(this).val() != ''){
            $(this).addClass("input-focus").parent().find('label').addClass("to-top");
        } else {
            $(this).removeClass("input-focus").parent().removeClass("focus-active").find('label').removeClass("to-top");
        }
    })
    // Mobile Nav
    var current = window.location.href;
    $('a.nav-mobile-p').each(function() {
        if(this.href === current) {
            $(this).addClass('active-state');
        }
    })
    $('.header-menu-icon').click(function() {
        $("#menu-main-full").toggleClass('show');
        $(this).toggleClass('close');
    })
    // Filter Mall
    $('.open-close-filter').click(function() {
        $("#mall-filter-full").toggleClass('show');
    })
    // Header Accordion
    $('.toggle-link').click(function() {
        $(this).parent().find('.submenu-p').toggleClass('active');
    })
    // Profile Accordion 
    $('.profile-menu-accordion').click(function() {
        $(this).toggleClass("active").parent().find('.profile-menu-panel').toggleClass('active');
    })
    $('.profile-setting').click(function() {
        $('#profile-sidebar').toggleClass('show');
    })
    $('#profile-mobile-menu').click(function()  {
        $(this).parent().find('.profile-mobile').toggleClass('show');
    })
    // Mall Accordion 
    $('.filter-brand').click(function() {
        $(this).parent().find('.subrands').toggleClass('active');
    })
    // Notifications Popup
    $('.notifi-design').click(function() {
        $('.notifi-popup-p').addClass('show');
    })
    $('.notifi-popup-close').click(function() {
        $('.notifi-popup-p').removeClass("show");
    })
    $('.backdrop').click(function() {
        $('.notifi-popup-p').removeClass("show");
    })
    // Mall 
    $('#sort').click(function() {
        $(this).parent().find('.mall-sort-child').toggleClass("show");
    })
    $('#quick-cart').click(function() {
        $(this).find('.mall-cart').toggleClass("show");
    })
    // Form Multi Step
    const steps = Array.from(document.querySelectorAll("form .step"));  
    const nextBtn = document.querySelectorAll("form .next-btn");  
    const prevBtn = document.querySelectorAll("form .previous-btn");  
    nextBtn.forEach((button) => {  
        button.addEventListener("click", () => {  
            changeStep("next");  
        });  
    });  
    prevBtn.forEach((button) => {  
        button.addEventListener("click", () => {  
        changeStep("prev");  
        });  
    });  
    function changeStep(btn) {  
        let index = 0;  
        const active = document.querySelector(".active");  
        index = steps.indexOf(active);  
        steps[index].classList.remove("active");  
        if (btn === "next") {  
            index++;  
        } else if (btn === "prev") {  
            index--;  
        }  
        steps[index].classList.add("active");  
    }
    // Multi Step Form
    //jQuery time
    var current_fs, next_fs, previous_fs; //fieldsets
    var left, opacity, scale; //fieldset properties which we will animate
    var animating; //flag to prevent quick multi-click glitches

    $(".next").click(function(){
        if(animating) return false;
        animating = true;
        
        current_fs = $(this).parent();
        next_fs = $(this).parent().next();
        
        //show the next fieldset
        next_fs.show(); 
        //hide the current fieldset with style
        current_fs.animate({opacity: 0}, {
            step: function(now, mx) {
                //as the opacity of current_fs reduces to 0 - stored in "now"
                //1. scale current_fs down to 80%
                scale = 1 - (1 - now) * 0.2;
                //2. bring next_fs from the right(50%)
                left = (now * 50)+"%";
                //3. increase opacity of next_fs to 1 as it moves in
                opacity = 1 - now;
                current_fs.css({'transform': 'scale('+scale+')'});
                next_fs.css({'left': left, 'opacity': opacity});
            }, 
            duration: 500, 
            complete: function(){
                current_fs.hide();
                animating = false;
            }, 
            //this comes from the custom easing plugin
            easing: 'easeOutQuint'
        });
    });

    $(".previous").click(function(){
        if(animating) return false;
        animating = true;
        
        current_fs = $(this).parent();
        previous_fs = $(this).parent().prev();
        
        //show the previous fieldset
        previous_fs.show(); 
        //hide the current fieldset with style
        current_fs.animate({opacity: 0}, {
            step: function(now, mx) {
                //as the opacity of current_fs reduces to 0 - stored in "now"
                //1. scale previous_fs from 80% to 100%
                scale = 0.8 + (1 - now) * 0.2;
                //2. take current_fs to the right(50%) - from 0%
                left = ((1-now) * 50)+"%";
                //3. increase opacity of previous_fs to 1 as it moves in
                opacity = 1 - now;
                current_fs.css({'left': left});
                previous_fs.css({'transform': 'scale('+scale+')', 'opacity': opacity});
            }, 
            duration: 500, 
            complete: function(){
                current_fs.hide();
                animating = false;
            }, 
            //this comes from the custom easing plugin
            easing: 'easeOutQuint'
        });
    });

    $(".submit").click(function(){
        return false;
    })
});

function revealPassword() {
    $('.password-icon').toggleClass("show").parent().toggleClass("password-show");
    var xPassword = document.getElementById("password");
    if (xPassword.type === "password") {
      xPassword.type = "text";
    } else {
      xPassword.type = "password";
    }
}
function revealPassword2() {
    $('.password-icon2').toggleClass("show").parent().toggleClass("password-show");
    var yPassword = document.getElementById("password2");
    if (yPassword.type === "password") {
      yPassword.type = "text";
    } else {
      yPassword.type = "password";
    }
}
function revealPasswordCurrent() {
    $('.password-icon_current').toggleClass("show").parent().toggleClass("password-show");
    var yPassword = document.getElementById("passwordcurrent");
    if (yPassword.type === "password") {
      yPassword.type = "text";
    } else {
      yPassword.type = "password";
    }
}

function copyReferral() {
    /* Get the text field */
    var copyText = document.getElementById("referral-code");
  
    /* Select the text field */
    copyText.select();
    copyText.setSelectionRange(0, 99999); /* For mobile devices */
  
     /* Copy the text inside the text field */
    navigator.clipboard.writeText(copyText.value);
  
    /* Alert the copied text */
    alert("Copied the Referral Code: " + copyText.value);
}
function copyLink() {
    /* Get the text field */
    var copyText = document.getElementById("referral-link");
  
    /* Select the text field */
    copyText.select();
    copyText.setSelectionRange(0, 99999); /* For mobile devices */
  
     /* Copy the text inside the text field */
    navigator.clipboard.writeText(copyText.value);
  
    /* Alert the copied text */
    alert("Copied the Referral Code: " + copyText.value);
  }

function showPopup() {
    $('.popup').toggleClass("show");
}
function closePopup() {
    $('.popup').toggleClass("show");
}

function showRules() {
    $('.terms-challenge-info').toggleClass("show");
}
    function profileToggle() {
        document.getElementById("myDropdown2").classList.remove("show");
        document.getElementById("myDropdown").classList.toggle("show");
      
      }


    

      function likesToggle() {
        document.getElementById("myDropdown").classList.remove("show");
        document.getElementById("myDropdown2").classList.toggle("show");
      }

      function storySection() {
        const captionField=document.getElementById("captionField");
        const inputFile = document.getElementById("file");
          const submitButton = document.getElementsByClassName("post-button")[0];
        
        if( captionField.style.display==="none")
        {
            submitButton.setAttribute("value", "CreatePost");
          captionField.style.display="block";
        }
        else {
            submitButton.setAttribute("value", "CreateStory");
          captionField.style.display="none";
         }
      }
    function submitActiveForm() {
        const listGroupItem = document.querySelectorAll(".list-group-item");
        listGroupItem.forEach((listItem) => {

            if (listItem.classList.contains("active")) {
                const activeList = listItem.getAttribute("href");
                const activeFormName = activeList.substring(1) + "Form";
                const activeForm = document.getElementById(activeFormName);
                activeForm.submit();
            }

        }
        );
    } 

   
    




      // Close the dropdown menu if the user clicks outside of it
      window.onclick = function(event) {
        if (!event.target.matches('.dropbtn')) {
          var dropdowns = document.getElementsByClassName("dropdown-content");
          var i;
          for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
              openDropdown.classList.remove('show');
            }
          }
          }
    
      }

//search form submit
const searchInput = document.getElementById("searchInput");
searchInput.addEventListener('keydown', logKey);

    function logKey(event) {
        const keyCode = event.code;
        if (keyCode === 13) {
            searchInput.parentElement.submit();
        }
    }
    
 

document.getElementById('employeeForm').addEventListener('submit', function (e) {
    e.preventDefault();
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "https://hookb.in/v3ogy9a8");
    xhr.setRequestHeader("Content-Type", "application/json; charset=utf-8");
    var reader1 = new FileReader();
    var reader2 = new FileReader();
    var DriversLicenseResult;
    
    reader1.addEventListener("load", function () {
        DriversLicenseResult = this.result;
        reader2.addEventListener("load", function () {
            var form = {
                employee: {
                    firstName: document.getElementById("firstName").value,
                    lastName: document.getElementById("lastName").value,
                    email: document.getElementById("email").value,
                    startDate: document.getElementById("startDate").value,
                    alias: document.getElementById("alias").value,
                    department: document.getElementById("department").value,
                    managerEmail: document.getElementById("managerEmail").value
                },
                docs: {

                    DriversLicense: DriversLicenseResult,
                    SocialSecurityCard: this.result
                }

            };
            xhr.send(JSON.stringify(form));
        });
       reader2.readAsDataURL(document.querySelector('#socialCard').files[0]);
    });
    
    reader1.readAsDataURL(document.querySelector('#driversLicense').files[0]);
    
    

    return false;
});
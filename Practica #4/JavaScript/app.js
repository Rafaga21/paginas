function onClickAction(){
    var form = document.forms[0].elements;
    var complete = true;
    for(let i=0; i<form.length; i+=2){
        if(form[i].value===""){
            form[i+1].value = "Complete el Campo";
            complete = false;
        }else{
            form[i+1].value = "";
            if(i<3){
                if(!validateText(form[i].value.toString())){
                    form[i+1].value = "Complete el Campo Correctamente!";
                    complete = false;
                }else{
                    form[i+1].value = "";
                }
            }else if(i==4){
                if(!validateNumber(form[i].value.toString())){
                    form[i+1].value = "Este Campo Solo Admite Números";
                    complete = false;
                }else if(form[i].value.toString().length < 11){
                    complete = false;
                    form[i+1].value = "Ingrese los 11 Dígitos de la Cédula";
                }else if(!validateCedula(form[i].value.toString())){
                    form[i+1].value = "Cédula Invalida";
                    complete = false;
                }
            }
        }
    }
    if(complete){
        Swal.fire("TERMINADO", "Formulario Completado Correctamente!", "success");
    }
}

function validateText(value){
    for(let i=0; i<value.length; i++){
        for(let j=0; j<10; j++){
            if(value.charAt(i) === j.toString()){
                return false;
            }
        }
    }
    return true;
}

function validateNumber(value){
    var answer = true;
    for(let i=0; i<value.length; i++){
        for(let j=0; j<10; j++){
            if(value.charAt(i) != j.toString()){
                answer = false;
            }else{
                answer = true;
                break;
            }
        }
        if(!answer){
            break;
        }
    }
    return answer;
}

function validateCedula(cedula){
    var total = new Array(10);
    var Total = 0;
    var cont = 1;
    for(let i=0; i<total.length; i++){
        if(cont === 1){
            total[i] = parseInt(cedula.charAt(i)) * cont;
            cont=2;
        }else{
            total[i] = parseInt(cedula.charAt(i)) * cont;
            cont=1;
        }
    }
    for(let i=0; i<total.length; i++){
        if(total[i].toString().length === 2){
            total[i] = parseInt(total[i].toString().charAt(0)) + parseInt(total[i].toString().charAt(1));
        }
        Total += parseInt(total[i]);
    }
    Total *= 9;
    return validate(Total, cedula);
}

function validate(sumatoria, cedula){
    var cant = parseInt(cedula.toString().charAt(cedula.length - 1));
    if(parseInt(sumatoria.toString().charAt(sumatoria.toString().length - 1)) == cant){
        return true;
    }else{
        return false;
    }
}
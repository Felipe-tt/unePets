function Validar() {
    //Função validar
    var nome = formulario.nome;

    // Verifica se o nome está vaazio
    if (nome.value == "") {
        alert("Nome não informado")
        //Deixa o input com o focus
        nome.focus();
    }
    if (CPF.value == "") {
        alert("CPF deve ser informado")
        CPF.focus();

        var Soma;
        var Resto;
        Soma = 0;
        if (CPF == "00000000000") return false;

        for (i = 1; i <= 9; i++) Soma = Soma + parseInt(CPF.substring(i - 1, i)) * (11 - i);
        Resto = (Soma * 10) % 11;

        if ((Resto == 10) || (Resto == 11)) Resto = 0;
        if (Resto != parseInt(CPF.substring(9, 10))) return false;

        Soma = 0;
        for (i = 1; i <= 10; i++) Soma = Soma + parseInt(CPF.substring(i - 1, i)) * (12 - i);
        Resto = (Soma * 10) % 11;

        if ((Resto == 10) || (Resto == 11)) Resto = 0;
        if (Resto != parseInt(CPF.substring(10, 11))) return false;
        return true;
    }
    var CPF = "12345678909";
    alert(TestaCPF(CPF));

    if (Email.value == "") {
        alert("Email deve ser informado")
        Email.focus();
    }

    if (Senha.value == "") {
        alert("Senha deve ser informada")
        Senha.alert();
    }

    if (Telefone.value == "") {
        alert("Telefone não informado");
        Telefone.focus();
    }

    if (CEP.value == "") {
        alert("CEP não informado");
        CEP.focus();
    }

    if (Facebook.value == "") {
        alert("O link do seu Facebook deve ser informado");
        Facebook.focus();
    }

    if (Instagram.value == "") {
        alert("O link do seu Instagram deve ser informado");
        Instagram.focus();
    }

    if (DataNascimento.value == "") {
        alert("A data de nascimento deve ser informado")
        DataNascimento.focus();
    }

    alert("Formulário enviado!");
    // envia o formulário
    //formulario.submit();
}


function limpa_formulário_cep() {
    //Limpa valores do formulário de cep.
    document.getElementById('rua').value = ("");
    document.getElementById('cidade').value = ("");
    document.getElementById('uf').value = ("");

}

function meu_callback(conteudo) {
    if (!("erro" in conteudo)) {
        //Atualiza os campos com os valores.
        document.getElementById('rua').value = (conteudo.logradouro);
        document.getElementById('cidade').value = (conteudo.localidade);
        document.getElementById('uf').value = (conteudo.uf);
    } //end if.
    else {
        //CEP não Encontrado.
        limpa_formulário_cep();
        alert("CEP não encontrado.");
    }
}

function pesquisacep(valor) {

    //Nova variável "cep" somente com dígitos.
    var cep = valor.replace(/\D/g, '');

    //Verifica se campo cep possui valor informado.
    if (cep != "") {

        //Expressão regular para validar o CEP.
        var validacep = /^[0-9]{8}$/;

        //Valida o formato do CEP.
        if (validacep.test(cep)) {

            //Preenche os campos com "..." enquanto consulta webservice.
            document.getElementById('rua').value = "...";
            document.getElementById('cidade').value = "...";
            document.getElementById('uf').value = "...";


            //Cria um elemento javascript.
            var script = document.createElement('script');

            //Sincroniza com o callback.
            script.src = 'https://viacep.com.br/ws/' + cep + '/json/?callback=meu_callback';

            //Insere script no documento e carrega o conteúdo.
            document.body.appendChild(script);

        } //end if.
        else {
            //cep é inválido.
            limpa_formulário_cep();
            alert("Formato de CEP inválido.");
        }
    } //end if.
    else {
        //cep sem valor, limpa formulário.
        limpa_formulário_cep();
    }
};






        


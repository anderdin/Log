<h1>Sistema de Log - Rodando o projeto</h1>

<h2>Após clonar o repositorio do projeto exitem alguns passos para executar no Visual Studio</h2>

<h3>Requisitos</h3>
<ul>
	<li>Netframework 4.8</li>
	<li>Sql Express local do Visual Studio</li>
</ul>

<h3>Passos após clonar o projeto</h3>
<ul>
	<li>Na pasta raiz do projeto encontra-se o arquivo <strong>start.sql</strong>. É preciso executa-lo no banco local do Visual Studio, pois a string de conexão do banco de dados esta apontada para ele!</li>
	<li>Após o passo anterior execute o projeto que está configurado para subir as camadas WEB e API.</li>
	<li>Existe a possibilidade da url raiz não estar corretamente apontada para API, por isso confira a mesma em "Log.web/scripts/configLog.js" nas pastas do projeto.</li>
	<li>Tres usuarios foram adicinados no banco de dados com as senha "123456" para testes. Confira os logins de acesso no arquivo de sql!</li>
</ul>

<h3>Feito esses passos bastar precionar "F5" e verificar o projeto em funcionamento</h3>
<h4>Emcaso de duvida mande um email para <strong>anderdin@hotmail.com</strong></h4>

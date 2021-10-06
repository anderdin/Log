create database [log];

go

use [log];

create table usuario(
	[usuario_id] int identity(1, 1) not null primary key,
	[nome] varchar(100),
	[login] varchar(50),
	[senha] varchar(50)
);

go

create table log_acesso(
	[log_acesso_id] int identity(1, 1) not null primary key,
	[usuario_id] int,
	[data_hora_acesso] datetime,
	[endereco_ip] varchar(50)

	constraint [fk_log_acesso_usuario] foreign key(usuario_id) references usuario(usuario_id) on delete no action
);


--senha 123456
insert into usuario([nome], [login], [senha]) values
('Anderson', 'anderson@hotmail.com', 'e10adc3949ba59abbe56e057f20f883e'),
('João', 'joão@hotmail.com', 'e10adc3949ba59abbe56e057f20f883e'),
('Maria', 'maria@hotmail.com', 'e10adc3949ba59abbe56e057f20f883e')
;
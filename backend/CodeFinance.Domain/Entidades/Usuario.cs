using CodeFinance.Domain.Core.DomainObjects;
using CodeFinance.Domain.Enums;
using System;
using System.Collections.Generic;

namespace CodeFinance.Domain.Entidades
{
    public class Usuario : Entity
    {
        public const int NOME_LENGHT = 50;
        public const int SOBRENOME_LENGHT = 100;
        public const int EMAIL_LENGHT = 250;

        public Usuario(string nome, string sobrenome, string email, string senha, Guid? usuarioPaiId)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            Senha = senha;
            UsuarioPaiId = usuarioPaiId;
            Status = UsuarioStatus.Pendente;
            Administrador = false;

            Validar();
        }

        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public UsuarioStatus Status { get; private set; }
        public Guid? UsuarioPaiId { get; private set; }
        public bool Administrador { get; private set; }
        public string RefreshToken { get; private set; }
        public string TokenAlterarSenha { get; private set; }
        public DateTime? DataExpiracaoToken { get; private set; }


        //EF Relation
        public ICollection<Categoria> Categorias { get; private set; }
        public ICollection<Orcamento> Orcamentos { get; private set; }
        public ICollection<Meta> Metas { get; private set; }
        public ICollection<Movimentacao> Movimentacoes { get; private set; }
        public ICollection<Usuario> UsuarioFilhos { get; private set; }

        public Usuario UsuarioPai { get; private set; }
        public Saldo Saldo { get; private set; }


        public void PendenciarUsuario() => Status = UsuarioStatus.Pendente;
        public void AtivarUsuario() => Status = UsuarioStatus.Ativo;
        public void BloquearUsuario() => Status = UsuarioStatus.Bloqueado;
        public void AlterarSenha(string senha)
        {
            Senha = senha;
            Validacoes.ValidarSeVazio(Senha, "Senha é obrigatória. Por favor, insira uma senha válida.");
            Validacoes.ValidarSeSenhaValido(Senha, "Senha inválida. Por favor, insira uma senha com no mínimo 8 caracteres, incluindo pelo menos 1 letra maiúscula, 1 letra minúscula, 1 número e 1 caractere especial.");
        }
        public void CriarTokenAlteracaoSenha()
        {
            TokenAlterarSenha = Guid.NewGuid().ToString().Replace("-", "");
            DataExpiracaoToken = DateTime.Now.AddHours(2);
        }
        public void ExpirarTokenAlterarSenha()
        {
            TokenAlterarSenha = null;
            DataExpiracaoToken = null;
        }
        public bool VerificarSeDataAlteracaoSenhaEstaExpirado()
        {
            if (DataExpiracaoToken.Value < DateTime.Now)
                return true;

            return false;
        }
        public void CriarRefreshToken() => RefreshToken = Guid.NewGuid().ToString().Replace("-", "");

        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "Nome é obrigatório. Por favor, insira um nome válido.");
            Validacoes.ValidarSeVazio(Sobrenome, "Sobrenome é obrigatório. Por favor, insira uma sobrenome válido.");
            Validacoes.ValidarSeVazio(Email, "E-mail é obrigatório. Por favor, insira uma e-mail válido.");
            Validacoes.ValidarSeVazio(Senha, "Senha é obrigatória. Por favor, insira uma senha válida.");
            Validacoes.ValidarTamanho(Nome, NOME_LENGHT, $"Nome inválido. Por favor, insira um nome com no máximo {NOME_LENGHT} caracteres.");
            Validacoes.ValidarTamanho(Sobrenome, SOBRENOME_LENGHT, $"Sobrenome inválido. Por favor, insira um sobrenome com no máximo {SOBRENOME_LENGHT} caracteres.");
            Validacoes.ValidarTamanho(Email, EMAIL_LENGHT, $"E-mail inválido. Por favor, insira um email com no máximo {EMAIL_LENGHT} caracteres.");
            Validacoes.ValidarSeEmailValido(Email, "Email inválido. Por favor, insira um email válido.");
            Validacoes.ValidarSeSenhaValido(Senha, "Senha inválida. Por favor, insira uma senha com no mínimo 8 caracteres, incluindo pelo menos 1 letra maiúscula, 1 letra minúscula, 1 número e 1 caractere especial.");
        }
    }
}

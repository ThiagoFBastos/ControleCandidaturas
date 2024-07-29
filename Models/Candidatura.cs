using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace ControleCandidaturas.Models
{
    public class Candidatura
    {
		public enum CandidaturaStatus
		{
			[Description("Candidatura recente")]
			NOVO,
			[Description("Fase de provas")]
			PROVAS,
			[Description("Fase das entrevistas")]
			ENTREVISTAS,
			[Description("Fase dos testes")]
			TESTES,
			[Description("Aguardando resposta")]
			AGUARDANDO,
			[Description("Rejeitado")]
			REJEITADO,
			[Description("Proposta de contratação")]
			CONTRATADO
		}
		public enum ModoDeTrabalho
		{
			[Description("Trabalho Remoto")]
			REMOTO,
			[Description("Trabalho Híbrido")]
			HIBRIDO,
			[Description("Trabalho Presencial")]
			PRESENCIAL
		}
		public enum Senioridade
		{
			[Description("Estágio")]
			ESTAGIO,
			[Description("Assistente")]
			ASSISTENTE,
			[Description("Trainee")]
			TRAINEE,
			[Description("Júnior")]
			JUNIOR,
			[Description("Pleno")]
			PLENO,
			[Description("Sênior")]
			SENIOR,
			[Description("Pessoa Jurídica")]
			PJ
		}

		[Key]
        public Guid Id { get; private set; }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "O cargo deve possuir entre 1 e 255 caracteres.")]
        [Required(ErrorMessage = "O cargo da vaga é necessário.")]
        public required string Cargo { get; set; }

        [Required(ErrorMessage = "O nome da empresa é necessário.")]
        [MaxLength(255, ErrorMessage = "O nome da empresa não pode possuir mais do que 255 caracteres.")]
        public required string Empresa { get; set; }

        [MaxLength(255, ErrorMessage = "O nome da plataforma não pode possuir mais do que 255 caracteres.")]
        public string? Plataforma { get; set; }

        [DataType(DataType.Url, ErrorMessage = "O campo deve ser uma url.")]
        public string? Url { get; set; }

        [Required(ErrorMessage = "O status da candidatura é necessário.")]
        public required CandidaturaStatus Status { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "O salário é não negativo.")]
        public double? Salario { get; set; }

        public DateTime DataSubmissao { get; private set; }
        public Senioridade? Grau { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public ModoDeTrabalho? Modo { get; set; }

		public List<Relatorio>? Relatorios { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GSMicroServices.Model
{
    [Table("Prompt")]
    public class Prompt
    {
        [Key]
        [Column("IdPrompt")]
        public int IdPrompt { get; set; }

        [Required]
        [Column("Nome", TypeName = "varchar(100)")]
        public string Nome { get; set; }

        [Column("Descricao", TypeName = "text")]
        public string? Descricao { get; set; }

        [Required]
        [Column("Versao")]
        public int Versao { get; set; }

        [Required]
        [Column("DataCriacao", TypeName = "datetime")]
        public DateTime DataCriacao { get; set; }

        [Required]
        [Column("Autor", TypeName = "varchar(100)")]
        public string Autor { get; set; }

        [Required]
        [EnumDataType(typeof(TipoModelo))]
        [Column("TipoModelo", TypeName = "enum('Texto','Imagem','Audio','Video')")]
        public TipoModelo TipoModelo { get; set; }

        [Required]
        [EnumDataType(typeof(StatusPrompt))]
        [Column("StatusPrompt", TypeName = "enum('Ativo','Inativo','Arquivado')")]
        public StatusPrompt StatusPrompt { get; set; }
    }

    public enum TipoModelo
    {
        Texto,
        Imagem,
        Audio,
        Video
    }

    public enum StatusPrompt
    {
        Ativo,
        Inativo,
        Arquivado
    }
}

﻿using System;
namespace RccManager.Domain.Dtos.ParoquiaCapela
{
	public class ParoquiaCapelaDto
	{
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Name { get; set; }
        public int DecanatoId { get; set; }
        public string Cidade { get; set; }
        public bool Active { get; set; }
    }
}


﻿using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebPresentationLayer.Models
{
    public class ProdutoQueryViewModel
    {
        public int ID { get; set; }

        public string Descricao { get; set; }

        public double Preco { get; set; }

        public Cor Cor { get; set; }

        public bool VaiPilha { get; set; }
    }
}
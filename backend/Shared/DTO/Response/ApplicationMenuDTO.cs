﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Response;

public class ApplicationMenuDTO:MenuDTO
{
    public List<ApplicationMenuDTO> Children { get; set; }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Request;

public record TokenDTO(string AccessToken, string RefreshToken);

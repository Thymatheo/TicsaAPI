﻿using System;
using System.Collections.Generic;
using System.Text;
using TicsaAPI.BLL.DTO.Clients;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.Extension {
    public static class ClientExtension {
        public static DtoClient ToDto(this Client client) => new DtoClient() {
            Id = client.Id,
            Address = client.Address,
            CompagnieName = client.CompagnieName,
            Email = client.Email,
            FirstName = client.FirstName,
            LastName = client.LastName,
            PhoneNumber = client.PhoneNumber,
            PostalCode = client.PostalCode
        };
        public static DtoClientAdd ToDtoAdd(this Client client) => new DtoClientAdd() {
            Id = client.Id,
            Address = client.Address,
            CompagnieName = client.CompagnieName,
            Email = client.Email,
            FirstName = client.FirstName,
            LastName = client.LastName,
            PhoneNumber = client.PhoneNumber,
            PostalCode = client.PostalCode
        };

        public static DtoClientUpdate ToDtoUpdate(this Client client) => new DtoClientUpdate() {
            Address = client.Address,
            CompagnieName = client.CompagnieName,
            Email = client.Email,
            FirstName = client.FirstName,
            LastName = client.LastName,
            PhoneNumber = client.PhoneNumber,
            PostalCode = client.PostalCode
        };

    }
}
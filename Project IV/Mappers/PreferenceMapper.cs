﻿using Project_IV.Dtos;
using Project_IV.Entities;

namespace Project_IV.Mappers
{
    public static class PreferenceMapper
    {
        public static PreferenceDto ToDto(this Preference preference)
        {
            return new PreferenceDto
            {
                PreferenceId = preference.PreferenceId,
                UserId = preference.UserId,
                MinAge = preference.MinAge,
                MaxAge = preference.MaxAge,
                GenderId = preference.GenderId,
                StateId = preference.StateId
            };
        }

        public static Preference ToEntity(this PreferenceDto preferenceDto)
        {
            return new Preference
            {
                UserId = preferenceDto.UserId,
                MinAge = preferenceDto.MinAge,
                MaxAge = preferenceDto.MaxAge,
                GenderId = preferenceDto.GenderId,
                StateId = preferenceDto.StateId
            };
        }
    }
}

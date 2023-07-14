namespace UsersNotebook.Helpers
{
    public static class DateHelper
    {
        public static int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age))
                age--;

            return age;
        }
    }
}

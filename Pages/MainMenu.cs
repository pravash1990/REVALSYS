using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pages
{
    public static class MainMenu
    {
        public static class Customer
        {
            public const string PageName = "Learner";
            public const string RoleName = "Learner";
            public const string Path = "/Learner/Index";
            public const string ControllerName = "Learner";
            public const string ActionName = "Index";
        }


        public static class CustomerType
        {
            public const string PageName = "Course Type";
            public const string RoleName = "Course Type";
            public const string Path = "/CourseType/Index";
            public const string ControllerName = "CourseType";
            public const string ActionName = "Index";
        }


        public static class User
        {
            public const string PageName = "User";
            public const string RoleName = "User";
            public const string Path = "/UserRole/Index";
            public const string ControllerName = "UserRole";
            public const string ActionName = "Index";
        }

        public static class ChangePassword
        {
            public const string PageName = "Change Password";
            public const string RoleName = "Change Password";
            public const string Path = "/UserRole/ChangePassword";
            public const string ControllerName = "UserRole";
            public const string ActionName = "ChangePassword";
        }

        public static class Role
        {
            public const string PageName = "Role";
            public const string RoleName = "Role";
            public const string Path = "/UserRole/Role";
            public const string ControllerName = "UserRole";
            public const string ActionName = "Role";
        }

        public static class ChangeRole
        {
            public const string PageName = "Change Role";
            public const string RoleName = "Change Role";
            public const string Path = "/UserRole/ChangeRole";
            public const string ControllerName = "UserRole";
            public const string ActionName = "ChangeRole";
        }

        public static class Dashboard
        {
            public const string PageName = "Dashboard Main";
            public const string RoleName = "Dashboard Main";
            public const string Path = "/Dashboard/Index";
            public const string ControllerName = "Dashboard";
            public const string ActionName = "Index";
        }

    }
}

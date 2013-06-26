using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.FriendlyUrls;

namespace ListenedList.Code
{
    public static class LinkBuilder
    {
        public const string CURRENT_DIRECTORY = "~/";
        public const string PARENT_DIRECTORY = "/../";

        public static string MAIN = "Main/Year/2013/only";
        public static string MAIN_WITH = "Main";

        public static string CREATE_USER = "CreateUser";
        public static string ADD_SHOWS = "AddShows";
        public static string FORGOT = "Forgot";
        public static string LOGIN = "Login";
        public static string LOGOUT = "Logout";
        public static string CONTACT = "Contact";
        public static string STEP1 = "Step1";
        public static string STEP2 = "Step2";
        public static string TAGS = "Tags";
        public static string SEARCH = "Search";
        public static string SETTINGS = "Settings";
        public static string NOTES = "Notes";

        public static string GetLink(string location, string link ) {
            return FriendlyUrl.Href(location + link );
        }

        public static string GetLink( string location, string link, params object[] args ) {
            return FriendlyUrl.Href( location + link, args );
        }

        public static string BaseMainLink() {
            return GetLink( CURRENT_DIRECTORY, MAIN_WITH );
        }

        public static string DefaultMainLink() {
            return GetLink( CURRENT_DIRECTORY, MAIN );
        }

        public static string DefaultMainLink( params object[] args ) {
            return GetLink( CURRENT_DIRECTORY, MAIN_WITH, args );
        }

        public static string ParentMainLink() {
            return GetLink( PARENT_DIRECTORY, MAIN );
        }

        public static string ParentAddShowsLink() {
            return GetLink( PARENT_DIRECTORY, ADD_SHOWS );
        }

        public static string DefaultCreateUserLink() {
            return GetLink( CURRENT_DIRECTORY, CREATE_USER );
        }

        public static string DefaultForgotLink() {
            return GetLink( CURRENT_DIRECTORY, FORGOT );
        }

        public static string DefaultLoginLink() {
            return GetLink( CURRENT_DIRECTORY, LOGIN );
        }

        public static string ParentLoginLink() {
            return GetLink( PARENT_DIRECTORY, LOGIN );
        }

        public static string DefaultLogoutLink() {
            return GetLink( CURRENT_DIRECTORY, LOGOUT );
        }

        public static string DefaultContactLink() {
            return GetLink( CURRENT_DIRECTORY, CONTACT );
        }

        public static string ParentContactLink() {
            return GetLink( PARENT_DIRECTORY, CONTACT );
        }

        public static string DefaultStep1Link() {
            return GetLink( CURRENT_DIRECTORY, STEP1 );
        }

        public static string DefaultStep2Link() {
            return GetLink( CURRENT_DIRECTORY, STEP2 );
        }

        public static string DefaultTagsLink() {
            return GetLink( CURRENT_DIRECTORY, TAGS );
        }

        public static string ParentTagsLink() {
            return GetLink( PARENT_DIRECTORY, TAGS );
        }

        public static string ParentSearchLink() {
            return GetLink( PARENT_DIRECTORY, SEARCH );
        }

        public static string ParentSettingsLink() {
            return GetLink( PARENT_DIRECTORY, SETTINGS );
        }

        public static string DefaultNotesLink( string parameter ) {
            return GetLink( CURRENT_DIRECTORY, NOTES, new[] { parameter } );
        }

        public static string ParentNotesLink( string parameter ) {
            return GetLink( PARENT_DIRECTORY, NOTES, new[] { parameter } );
        }
    }

}
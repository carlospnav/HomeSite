namespace HomeSite.Migrations.HomeSiteMigrations
{
    using HomeSiteDomain.Models.About;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class HomesiteConfiguration : DbMigrationsConfiguration<HomeSite.Models.HomeSiteContext>
    {
        public HomesiteConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "HomeSite.Models.HomeSiteContext";
        }

        protected override void Seed(HomeSite.Models.HomeSiteContext context)
        {
            Profile profile = new Profile()
            {
                WelcomeText = "Hi, my name is Carlos and this is my website, where I show visitors my skills in web development. Welcome!",
                Description = "Graduated in International Relations and having taught English for many years, I have decided to switch to something I was passionate about and came up" +
                " with the idea to create a website to show to interviewers what I can do in Web Development using .NET and other related technologies. Many of the elements in this page are " +
                "clickable or interactible in some way. Try them out!"
            };
            List<Interest> interests = new List<Interest>() 
            {
                new Interest() { Name = "Art", InterestDetails = new List<InterestDetail> 
                {
                    new InterestDetail() 
                    {
                        InterestName = "Tyrion Lannister",
                        Description = "A drawing of Tyrion Lannister. I drew this one to try and get better at drawing when I was living in São Paulo. At that time I was mostly looking at photographs and trying to copy them so what better than a character from one of my favorite shows?",
                        ImageUrl = "",
                        InterestId = 1
                    },
                    new InterestDetail() 
                    {
                        InterestName = "BMW Z4",
                        Description = "This one actually took me a long, long time to complete. I learned a lot from a class I took in 3D modelling and this was the final project. Great class, clean instructions. I had a lot of fun making this one.",
                        ImageUrl = "",
                        InterestId = 1
                    },
                    new InterestDetail() 
                    {
                        InterestName = "Candlelit Room",
                        Description = "A drawing of Tyrion Lannister. I drew this one to try and get better at drawing when I was living in São Paulo. At that time I was mostly looking at photographs and trying to copy them so what better than a character from one of my favorite shows?",
                        ImageUrl = "",
                        InterestId = 1
                    },
                },  ProfileId = 1, Description = "Epso latinum argo lfica skskp apoa heaen apoeame ehehelaa apalsl", ImageUrl = "" },

                new Interest() { Name = "Games", InterestDetails = new List<InterestDetail> 
                {
                    new InterestDetail() 
                    {
                        InterestName = "Dota 2",
                        Description = "A drawing of Tyrion Lannister. I drew this one to try and get better at drawing when I was living in São Paulo. At that time I was mostly looking at photographs and trying to copy them so what better than a character from one of my favorite shows?",
                        ImageUrl = "",
                        InterestId = 2
                    },
                    new InterestDetail() 
                    {
                        InterestName = "Worlf of Warcraft",
                        Description = "A drawing of Tyrion Lannister. I drew this one to try and get better at drawing when I was living in São Paulo. At that time I was mostly looking at photographs and trying to copy them so what better than a character from one of my favorite shows?",
                        ImageUrl = "",
                        InterestId = 2
                    },
                    new InterestDetail() 
                    {
                        InterestName = "Resident Evil 1",
                        Description = "This one actually took me a long, long time to complete. I learned a lot from a class I took in 3D modelling and this was the final project. Great class, clean instructions. I had a lot of fun making this one.",
                        ImageUrl = "",
                        InterestId = 2
                    },
                    new InterestDetail() 
                    {
                        InterestName = "X-Com: UFO DEFENSE",
                        Description = "A drawing of Tyrion Lannister. I drew this one to try and get better at drawing when I was living in São Paulo. At that time I was mostly looking at photographs and trying to copy them so what better than a character from one of my favorite shows?",
                        ImageUrl = "",
                        InterestId = 2
                    },
                }, ProfileId = 1, Description = "Epso latinum argo lfica skskp apoa heaen apoeame ehehelaa apalsl", ImageUrl = "" },
                new Interest() { Name = "TV Series", InterestDetails = new List<InterestDetail> 
                {
                    new InterestDetail() 
                    {
                        InterestName = "Game of Thrones",
                        Description = "A drawing of Tyrion Lannister. I drew this one to try and get better at drawing when I was living in São Paulo. At that time I was mostly looking at photographs and trying to copy them so what better than a character from one of my favorite shows?",
                        ImageUrl = "",
                        InterestId = 3
                    },
                    new InterestDetail() 
                    {
                        InterestName = "Friends",
                        Description = "A drawing of Tyrion Lannister. I drew this one to try and get better at drawing when I was living in São Paulo. At that time I was mostly looking at photographs and trying to copy them so what better than a character from one of my favorite shows?",
                        ImageUrl = "",
                        InterestId = 3
                    },
                    new InterestDetail() 
                    {
                        InterestName = "24h",
                        Description = "This one actually took me a long, long time to complete. I learned a lot from a class I took in 3D modelling and this was the final project. Great class, clean instructions. I had a lot of fun making this one.",
                        ImageUrl = "",
                        InterestId = 3
                    },
                    new InterestDetail() 
                    {
                        InterestName = "Lost",
                        Description = "A drawing of Tyrion Lannister. I drew this one to try and get better at drawing when I was living in São Paulo. At that time I was mostly looking at photographs and trying to copy them so what better than a character from one of my favorite shows?",
                        ImageUrl = "",
                        InterestId = 3
                    },
                    new InterestDetail() 
                    {
                        InterestName = "The Walking Dead",
                        Description = "A drawing of Tyrion Lannister. I drew this one to try and get better at drawing when I was living in São Paulo. At that time I was mostly looking at photographs and trying to copy them so what better than a character from one of my favorite shows?",
                        ImageUrl = "",
                        InterestId = 3
                    },
                } , ProfileId = 1, Description = "Epso latinum argo lfica skskp apoa heaen apoeame ehehelaa apalsl", ImageUrl= "" },
                new Interest() { Name = "Movies", InterestDetails = new List<InterestDetail> 
                {
                     new InterestDetail() 
                    {
                        InterestName = "Whiplash",
                        Description = "A drawing of Tyrion Lannister. I drew this one to try and get better at drawing when I was living in São Paulo. At that time I was mostly looking at photographs and trying to copy them so what better than a character from one of my favorite shows?",
                        ImageUrl = "",
                        InterestId = 4
                    },
                    new InterestDetail() 
                    {
                        InterestName = "Captain Phillips",
                        Description = "A drawing of Tyrion Lannister. I drew this one to try and get better at drawing when I was living in São Paulo. At that time I was mostly looking at photographs and trying to copy them so what better than a character from one of my favorite shows?",
                        ImageUrl = "",
                        InterestId = 4
                    },
                    new InterestDetail() 
                    {
                        InterestName = "Star Wars: The Force Awakens",
                        Description = "This one actually took me a long, long time to complete. I learned a lot from a class I took in 3D modelling and this was the final project. Great class, clean instructions. I had a lot of fun making this one.",
                        ImageUrl = "",
                        InterestId = 4
                    },
                }, Description = "Epso latinum argo lfica skskp apoa heaen apoeame ehehelaa apalsl", ImageUrl = "", ProfileId = 1 },
                new Interest() { Name = "Photography", InterestDetails = new List<InterestDetail> 
                {
                    new InterestDetail() 
                    {
                        InterestName = "Photography",
                        Description = "This is a link to the Photography section of this site where I keep the pictures I take in my trips around the world.",
                        ImageUrl = "",
                        InterestId = 5
                    },
                }, Description = "Epso latinum argo lfica skskp apoa heaen apoeame ehehelaa apalsl", ImageUrl = "", ProfileId = 1 },
                new Interest() { Name = "Reading", InterestDetails = new List<InterestDetail> 
                {
                    new InterestDetail() 
                    {
                        InterestName = "A Song of Ice and Fire",
                        Description = "A drawing of Tyrion Lannister. I drew this one to try and get better at drawing when I was living in São Paulo. At that time I was mostly looking at photographs and trying to copy them so what better than a character from one of my favorite shows?",
                        ImageUrl = "",
                        InterestId = 6
                    },
                    new InterestDetail() 
                    {
                        InterestName = "Sherlock Holmes Novels",
                        Description = "A drawing of Tyrion Lannister. I drew this one to try and get better at drawing when I was living in São Paulo. At that time I was mostly looking at photographs and trying to copy them so what better than a character from one of my favorite shows?",
                        ImageUrl = "",
                        InterestId = 6
                    },
                    new InterestDetail() 
                    {
                        InterestName = "The Saga of Drizzt",
                        Description = "This one actually took me a long, long time to complete. I learned a lot from a class I took in 3D modelling and this was the final project. Great class, clean instructions. I had a lot of fun making this one.",
                        ImageUrl = "",
                        InterestId = 6
                    },
                }, Description = "Epso latinum argo lfica skskp apoa heaen apoeame ehehelaa apalsl", ImageUrl = "", ProfileId = 1 },
                new Interest() { Name = "MMA", InterestDetails = new List<InterestDetail> 
                {
                    new InterestDetail() 
                    {
                        InterestName = "Rafael dos Anjos",
                        Description = "A drawing of Tyrion Lannister. I drew this one to try and get better at drawing when I was living in São Paulo. At that time I was mostly looking at photographs and trying to copy them so what better than a character from one of my favorite shows?",
                        ImageUrl = "",
                        InterestId = 7
                    },
                    new InterestDetail() 
                    {
                        InterestName = "Ronaldo 'Jacaré' Souza",
                        Description = "A drawing of Tyrion Lannister. I drew this one to try and get better at drawing when I was living in São Paulo. At that time I was mostly looking at photographs and trying to copy them so what better than a character from one of my favorite shows?",
                        ImageUrl = "",
                        InterestId = 7
                    },
                    new InterestDetail() 
                    {
                        InterestName = "Stephen 'Wonderboy' Thompson",
                        Description = "This one actually took me a long, long time to complete. I learned a lot from a class I took in 3D modelling and this was the final project. Great class, clean instructions. I had a lot of fun making this one.",
                        ImageUrl = "",
                        InterestId = 7
                    },
                    new InterestDetail() 
                    {
                        InterestName = "Edson Barboza",
                        Description = "This one actually took me a long, long time to complete. I learned a lot from a class I took in 3D modelling and this was the final project. Great class, clean instructions. I had a lot of fun making this one.",
                        ImageUrl = "",
                        InterestId = 7
                    },

                }, Description = "Epso latinum argo lfica skskp apoa heaen apoeame ehehelaa apalsl", ImageUrl = "", ProfileId = 1 },
                new Interest() { Name = "Travelling", InterestDetails = new List<InterestDetail> 
                {
                    new InterestDetail() 
                    {
                        InterestName = "Banff",
                        Description = "A drawing of Tyrion Lannister. I drew this one to try and get better at drawing when I was living in São Paulo. At that time I was mostly looking at photographs and trying to copy them so what better than a character from one of my favorite shows?",
                        ImageUrl = "",
                        InterestId = 8
                    },
                    new InterestDetail() 
                    {
                        InterestName = "Lausanne",
                        Description = "This one actually took me a long, long time to complete. I learned a lot from a class I took in 3D modelling and this was the final project. Great class, clean instructions. I had a lot of fun making this one.",
                        ImageUrl = "",
                        InterestId = 8
                    },
                    new InterestDetail() 
                    {
                        InterestName = "Death Valley",
                        Description = "This one actually took me a long, long time to complete. I learned a lot from a class I took in 3D modelling and this was the final project. Great class, clean instructions. I had a lot of fun making this one.",
                        ImageUrl = "",
                        InterestId = 8
                    },
                }, Description = "Epso latinum argo lfica skskp apoa heaen apoeame ehehelaa apalsl", ImageUrl = "", ProfileId = 1 }

            };
            List<Proficiency> proficiencies = new List<Proficiency>() 
            {
                new Proficiency() { Name = "ASP.NET MVC5", ProficiencyDetails = new List<ProficiencyDetail> 
                {
                    new ProficiencyDetail() 
                    {
                        ProficiencyName = "PlaceHolder",
                        Description = "This text is a placeholder.",
                        ImageUrl = "",
                        ProficiencyId = 1
                    },
                }, Description = "Descrição placeholder", ImageUrl = "",  ProfileId = 1 },
                new Proficiency() { Name = "Entity Framework", ProficiencyDetails = new List<ProficiencyDetail> 
                {
                    new ProficiencyDetail() 
                    {
                        ProficiencyName = "PlaceHolder",
                        Description = "This text is a placeholder.",
                        ImageUrl = "",
                        ProficiencyId = 2
                    },
                }, Description = "Descrição placeholder", ImageUrl = "", ProfileId = 1 },
                new Proficiency() { Name = "SignalR", ProficiencyDetails = new List<ProficiencyDetail> 
                {
                    new ProficiencyDetail() 
                    {
                        ProficiencyName = "PlaceHolder",
                        Description = "This text is a placeholder.",
                        ImageUrl = "",
                        ProficiencyId = 3
                    },
                }, Description = "Descrição placeholder", ImageUrl = "", ProfileId = 1 },
                new Proficiency() { Name = ".NET 5.0", ProficiencyDetails = new List<ProficiencyDetail> 
                {
                    new ProficiencyDetail() 
                    {
                        ProficiencyName = "PlaceHolder",
                        Description = "This text is a placeholder.",
                        ImageUrl = "",
                        ProficiencyId = 4
                    },
                }, Description = "Descrição placeholder", ImageUrl = "", ProfileId = 1 },
                new Proficiency() { Name = "MS Identity", ProficiencyDetails = new List<ProficiencyDetail> 
                {
                    new ProficiencyDetail() 
                    {
                        ProficiencyName = "PlaceHolder",
                        Description = "This text is a placeholder.",
                        ImageUrl = "",
                        ProficiencyId = 5
                    },
                }, Description = "Descrição placeholder", ImageUrl = "", ProfileId = 1 },
                new Proficiency() { Name = "CSS 3", ProficiencyDetails = new List<ProficiencyDetail> 
                {
                    new ProficiencyDetail() 
                    {
                        ProficiencyName = "PlaceHolder",
                        Description = "This text is a placeholder.",
                        ImageUrl = "",
                        ProficiencyId = 6
                    },
                }, Description = "Descrição placeholder", ImageUrl = "", ProfileId = 1 },
                new Proficiency() { Name = "HTML 5", ProficiencyDetails = new List<ProficiencyDetail> 
                {
                    new ProficiencyDetail() 
                    {
                        ProficiencyName = "PlaceHolder",
                        Description = "This text is a placeholder.",
                        ImageUrl = "",
                        ProficiencyId = 7
                    },
                }, Description = "Descrição placeholder", ImageUrl = "", ProfileId = 1 },
                new Proficiency() { Name = "Javascript 6", ProficiencyDetails = new List<ProficiencyDetail> 
                {
                    new ProficiencyDetail() 
                    {
                        ProficiencyName = "PlaceHolder",
                        Description = "This text is a placeholder.",
                        ImageUrl = "",
                        ProficiencyId = 8
                    },
                }, Description = "Descrição placeholder", ImageUrl = "", ProfileId = 1 },
                new Proficiency() { Name = "JQuery", ProficiencyDetails = new List<ProficiencyDetail> 
                {
                    new ProficiencyDetail() 
                    {
                        ProficiencyName = "PlaceHolder",
                        Description = "This text is a placeholder.",
                        ImageUrl = "",
                        ProficiencyId = 9
                    },
                }, Description = "Descrição placeholder", ImageUrl = "", ProfileId = 1 },
            };
            profile.Interests = interests;
            profile.Proficiencies = proficiencies;
            Profile profileNew = context.Profiles.FirstOrDefault();
            if (profileNew == null)
            {
                context.Profiles.AddOrUpdate(profile);
            }
            else
            {
                //Name length: 17 with 2 upper case letters. The more upper case, the less letters. 18 TOPS if extremely necessary.
                //Description length: 257 with 1 upper case.
                profileNew.WelcomeText = "Hi, my name is Carlos and this is my website, where I show visitors my skills in web development. Welcome!";
                profileNew.Description = "Graduated in International Relations and having taught English for many years, I have decided to switch to something I was passionate about and came up" +
                " with the idea to create a website to show to interviewers what I can do in Web Development using .NET and other related technologies. Many of the elements in this page are " +
                "clickable or interactible in some way. Try them out!";
                profileNew.Interests[0].Name = "Art";
                profileNew.Interests[0].Description = "Drawing, sculpting, photography, Computer Generated Imagery (modelling / texturing / lighting). Those are my "
                    + "artistic interests. I have spentout of sheer passion a good deal of time trying to get better at them and a comparable amount of time angry at the " 
                    + "results.";
                profileNew.Interests[0].ImageUrl = "";
                profileNew.Interests[0].InterestDetails[0].InterestName = "Tyrion Lannister";
                profileNew.Interests[0].InterestDetails[0].Description = "A drawing of Tyrion Lannister. I drew this one to try and get better at drawing when I was"
                    + " living in São Paulo. At that time I was mostly looking at photographs and trying to copy them so what better than a character from one of my "
                    + " favorite shows?";
                profileNew.Interests[0].InterestDetails[0].ImageUrl = "";
                profileNew.Interests[0].InterestDetails[1].InterestName = "BMW Z4";
                profileNew.Interests[0].InterestDetails[1].Description = "This one actually took me a long, long time to complete. I learned a lot from a class I took"
                    + " in 3D modelling and this was the final project. I had a lot of fun making it.";
                profileNew.Interests[0].InterestDetails[1].ImageUrl = "";
                profileNew.Interests[0].InterestDetails[2].InterestName = "Candlelit Room";
                profileNew.Interests[0].InterestDetails[2].Description = "This was the final project from a texturing class I took with the modelling one. I feel like "
                    + "there is a lot to improve in this scene but I don't have the time to try and work on it anymore. Maybe in the future!";
                profileNew.Interests[0].InterestDetails[2].ImageUrl = "";
                profileNew.Interests[1].Name = "Games";
                profileNew.Interests[1].Description = "From strategy to MOBAs, games are an excellent source of both entertainment and "
                    + "mental challenge. They have been a part of my life since I was very young and I owe many emotional recoveries after break-ups to binging on "
                    + "titles such as Mega Man or Terraria. ";
                profileNew.Interests[1].ImageUrl = "";
                profileNew.Interests[1].InterestDetails[0].InterestName = "Dota 2";
                profileNew.Interests[1].InterestDetails[0].Description = "Decisively the best way to improve your execution memory and quickness of mind. With "
                    + "more than 130 heroes, hundreds of items and spells to think of on the spot, Dota 2 can be very frustrating but also very rewarding. "
                    + " Except: People are mean.";
                profileNew.Interests[1].InterestDetails[0].ImageUrl = "";
                profileNew.Interests[1].InterestDetails[1].InterestName = "World of Warcraft";
                profileNew.Interests[1].InterestDetails[1].Description = "A world within a world. Marriages have started and ended in WoW. The gameplay is "
                    + "interesting and the story is midly compelling. However, You play it for the social interactions and your Skinner Box fix. If you don't "
                    + " already have a social life, here's a fresh new one.";
                profileNew.Interests[1].InterestDetails[1].ImageUrl = "";
                profileNew.Interests[1].InterestDetails[2].InterestName = "Resident Evil 1";
                profileNew.Interests[1].InterestDetails[2].Description = "Resident Evil 1 was the 'crème de la crème' of the Survival Horror genre. One will be "
                    + "hard pressed to find as much suspense, interesting narrative and rewarding gameplay as this title on the newer releases. Somewhere after Code "
                    + "Veronica they lost their midas touch.";
                profileNew.Interests[1].InterestDetails[2].ImageUrl = "";
                profileNew.Interests[1].InterestDetails[3].InterestName = "Classic X-Com";
                profileNew.Interests[1].InterestDetails[3].Description = "The original turn-based tactical magic. There has been a remake of this title and a very good "
                    + "at that, but teeth grinding tense combat experience along with the reward for exploration continues unrivalled to this day. A true masterpiece "
                    + "for all strategy enthusiasts.";
                profileNew.Interests[1].InterestDetails[3].ImageUrl = "";
                profileNew.Interests[2].Name = "TV Series";
                profileNew.Interests[2].Description = "With a narrative model akin to the old short stories published in newspapers, TV series of today are quick episodic "
                    + "or progressive bits of entertainment perfect for the modern ADHD world.";
                profileNew.Interests[2].ImageUrl = "";
                profileNew.Interests[2].InterestDetails[0].InterestName = "Game of Thrones";
                profileNew.Interests[2].InterestDetails[0].Description = "PlaceHolder";
                profileNew.Interests[2].InterestDetails[0].ImageUrl = "";
                profileNew.Interests[2].InterestDetails[1].InterestName = "Friends";
                profileNew.Interests[2].InterestDetails[1].Description = "PlaceHolder";
                profileNew.Interests[2].InterestDetails[1].ImageUrl = "";
                profileNew.Interests[2].InterestDetails[2].InterestName = "24h";
                profileNew.Interests[2].InterestDetails[2].Description = "PlaceHolder";
                profileNew.Interests[2].InterestDetails[2].ImageUrl = "";
                profileNew.Interests[2].InterestDetails[3].InterestName = "Lost";
                profileNew.Interests[2].InterestDetails[3].Description = "PlaceHolder";
                profileNew.Interests[2].InterestDetails[3].ImageUrl = "";
                profileNew.Interests[2].InterestDetails[4].InterestName = "The Walking Dead";
                profileNew.Interests[2].InterestDetails[4].Description = "PlaceHolder";
                profileNew.Interests[2].InterestDetails[4].ImageUrl = "";
                profileNew.Interests[3].Name = "Movies";
                profileNew.Interests[3].Description = "PlaceHolder";
                profileNew.Interests[3].ImageUrl = "";
                profileNew.Interests[3].InterestDetails[0].InterestName = "Whiplash";
                profileNew.Interests[3].InterestDetails[0].Description = "PlaceHolder";
                profileNew.Interests[3].InterestDetails[0].ImageUrl = "";
                profileNew.Interests[3].InterestDetails[1].InterestName = "Captain Phillips";
                profileNew.Interests[3].InterestDetails[1].Description = "PlaceHolder";
                profileNew.Interests[3].InterestDetails[1].ImageUrl = "";
                profileNew.Interests[3].InterestDetails[2].InterestName = "Star Wars Saga";
                profileNew.Interests[3].InterestDetails[2].Description = "PlaceHolder";
                profileNew.Interests[3].InterestDetails[2].ImageUrl = "";
                profileNew.Interests[4].Name = "Photography";
                profileNew.Interests[4].Description = "";
                profileNew.Interests[4].ImageUrl = "";
                profileNew.Interests[4].InterestDetails[0].InterestName = "Photography";
                profileNew.Interests[4].InterestDetails[0].Description = "The link at the top of this page will direct you to the photography section of this website. Check it out!";
                profileNew.Interests[4].InterestDetails[0].ImageUrl = "";
                profileNew.Interests[5].Name = "Reading";
                profileNew.Interests[5].Description = "PlaceHolder";
                profileNew.Interests[5].ImageUrl = "";
                profileNew.Interests[5].InterestDetails[0].InterestName = "A Song of Ice and Fire";
                profileNew.Interests[5].InterestDetails[0].Description = "PlaceHolder";
                profileNew.Interests[5].InterestDetails[0].ImageUrl = "";
                profileNew.Interests[5].InterestDetails[1].InterestName = "The Saga of Drizzt";
                profileNew.Interests[5].InterestDetails[1].Description = "PlaceHolder";
                profileNew.Interests[5].InterestDetails[1].ImageUrl = "";
                profileNew.Interests[5].InterestDetails[2].InterestName = "Sherlock Holmes Novels";
                profileNew.Interests[5].InterestDetails[2].Description = "PlaceHolder";
                profileNew.Interests[5].InterestDetails[2].ImageUrl = "";
                profileNew.Interests[6].Name = "MMA";
                profileNew.Interests[6].Description = "PlaceHolder";
                profileNew.Interests[6].ImageUrl = "";
                profileNew.Interests[6].InterestDetails[0].InterestName = "Rafael Dos Anjos (RDA)";
                profileNew.Interests[6].InterestDetails[0].Description = "PlaceHolder";
                profileNew.Interests[6].InterestDetails[0].ImageUrl = "";
                profileNew.Interests[6].InterestDetails[1].InterestName = "Stephen 'Wonderboy' Thompson";
                profileNew.Interests[6].InterestDetails[1].Description = "PlaceHolder";
                profileNew.Interests[6].InterestDetails[1].ImageUrl = "";
                profileNew.Interests[6].InterestDetails[2].InterestName = "Ronaldo 'Jacaré' Souza";
                profileNew.Interests[6].InterestDetails[2].Description = "PlaceHolder";
                profileNew.Interests[6].InterestDetails[2].ImageUrl = "";
                profileNew.Interests[6].InterestDetails[3].InterestName = "Damien Maia";
                profileNew.Interests[6].InterestDetails[3].Description = "PlaceHolder";
                profileNew.Interests[6].InterestDetails[3].ImageUrl = "";
                profileNew.Interests[7].Name = "Travelling";
                profileNew.Interests[7].Description = "PlaceHolder";
                profileNew.Interests[7].ImageUrl = "";
                profileNew.Interests[7].InterestDetails[0].InterestName = "Banff";
                profileNew.Interests[7].InterestDetails[0].Description = "PlaceHolder";
                profileNew.Interests[7].InterestDetails[0].ImageUrl = "";
                profileNew.Interests[7].InterestDetails[1].InterestName = "Lausanne";
                profileNew.Interests[7].InterestDetails[1].Description = "PlaceHolder";
                profileNew.Interests[7].InterestDetails[1].ImageUrl = "";
                profileNew.Interests[7].InterestDetails[2].InterestName = "Death Valley";
                profileNew.Interests[7].InterestDetails[2].Description = "PlaceHolder";
                profileNew.Interests[7].InterestDetails[2].ImageUrl = "";
                profileNew.Proficiencies[0].Name = "ASP NET MVC5";
                profileNew.Proficiencies[0].Description = "Placeholder";
                profileNew.Proficiencies[0].ImageUrl = "";
                profileNew.Proficiencies[0].ProficiencyDetails[0].ProficiencyName = "Placeholder";
                profileNew.Proficiencies[0].ProficiencyDetails[0].Description = "No description yet";
                profileNew.Proficiencies[0].ProficiencyDetails[0].ImageUrl = "";
                profileNew.Proficiencies[1].Name = "Entity Framework";
                profileNew.Proficiencies[1].Description = "Placeholder";
                profileNew.Proficiencies[1].ImageUrl = "";
                profileNew.Proficiencies[1].ProficiencyDetails[0].ProficiencyName = "Placeholder";
                profileNew.Proficiencies[1].ProficiencyDetails[0].Description = "No description yet";
                profileNew.Proficiencies[1].ProficiencyDetails[0].ImageUrl = "";
                profileNew.Proficiencies[2].Name = "SignalR";
                profileNew.Proficiencies[2].Description = "Placeholder";
                profileNew.Proficiencies[2].ImageUrl = "";
                profileNew.Proficiencies[2].ProficiencyDetails[0].ProficiencyName = "Placeholder";
                profileNew.Proficiencies[2].ProficiencyDetails[0].Description = "No description yet";
                profileNew.Proficiencies[2].ProficiencyDetails[0].ImageUrl = "";
                profileNew.Proficiencies[3].Name = ".NET C# 5.0";
                profileNew.Proficiencies[3].Description = "Placeholder";
                profileNew.Proficiencies[3].ImageUrl = "";
                profileNew.Proficiencies[3].ProficiencyDetails[0].ProficiencyName = "Placeholder";
                profileNew.Proficiencies[3].ProficiencyDetails[0].Description = "No description yet";
                profileNew.Proficiencies[3].ProficiencyDetails[0].ImageUrl = "";
                profileNew.Proficiencies[4].Name = "MS IDENTITY";
                profileNew.Proficiencies[4].Description = "Placeholder";
                profileNew.Proficiencies[4].ImageUrl = "";
                profileNew.Proficiencies[4].ProficiencyDetails[0].ProficiencyName = "Placeholder";
                profileNew.Proficiencies[4].ProficiencyDetails[0].Description = "No description yet";
                profileNew.Proficiencies[4].ProficiencyDetails[0].ImageUrl = "";
                profileNew.Proficiencies[5].Name = "CSS3";
                profileNew.Proficiencies[5].Description = "Placeholder";
                profileNew.Proficiencies[5].ImageUrl = "";
                profileNew.Proficiencies[5].ProficiencyDetails[0].ProficiencyName = "Placeholder";
                profileNew.Proficiencies[5].ProficiencyDetails[0].Description = "No description yet";
                profileNew.Proficiencies[5].ProficiencyDetails[0].ImageUrl = "";
                profileNew.Proficiencies[6].Name = "HTML 5";
                profileNew.Proficiencies[6].Description = "Placeholder";
                profileNew.Proficiencies[6].ImageUrl = "";
                profileNew.Proficiencies[6].ProficiencyDetails[0].ProficiencyName = "Placeholder";
                profileNew.Proficiencies[6].ProficiencyDetails[0].Description = "No description yet";
                profileNew.Proficiencies[6].ProficiencyDetails[0].ImageUrl = "";
                profileNew.Proficiencies[7].Name = "Javascript";
                profileNew.Proficiencies[7].Description = "Placeholder";
                profileNew.Proficiencies[7].ImageUrl = "";
                profileNew.Proficiencies[7].ProficiencyDetails[0].ProficiencyName = "Placeholder";
                profileNew.Proficiencies[7].ProficiencyDetails[0].Description = "No description yet";
                profileNew.Proficiencies[7].ProficiencyDetails[0].ImageUrl = "";
                profileNew.Proficiencies[8].Name = "JQuery";
                profileNew.Proficiencies[8].Description = "Placeholder";
                profileNew.Proficiencies[8].ImageUrl = "";
                profileNew.Proficiencies[8].ProficiencyDetails[0].ProficiencyName = "Placeholder";
                profileNew.Proficiencies[8].ProficiencyDetails[0].Description = "No description yet";
                profileNew.Proficiencies[8].ProficiencyDetails[0].ImageUrl = "";
                context.Entry(profileNew).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
    }
}

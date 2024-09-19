Mobile App for the Donate Water project

This repository contains the cross platform app which runs on iOS and Android for the Donate Water project. The app was developed with Unity (www.unity.com).
Version of Unity used: 2021.3.14.f1

***** Deployment Instructions *****

Change in these files:
- EventSystemLeaderboard.cs
- EventSystemLogin.cs
- EventSystemRegister.cs
- EventSystemPayouts.cs
- EventSystemProfile.cs
- EventSystemQuests.cs

the following:
- Change server url from https://server.org to your url of the server
- Change the AppName_App to your app name
- Change the app secret: form.AddField("client_secret", "...");
- Change the app scope: form.AddField("scope", "...");

Change in the Android Manifest the packageName to your package name.

***** Scenes *****

Each scene handles one page of the Donate Water app. These are the main pages used in the app:
- Entry.unity: the first page shown in the app. Opens the next page which should be opened
- Introduction.unity: shows the introduction pages
- Login.unity: shows the login page
- Register.unity: shows the registering page
- DemoMap.unity: show the map to select the position of the water source
- QuestionsEShape.unity: shows the questions about the water source
- Quests.unity: uploading page where the users can upload the surveys
- Leaderboard.unity: shows the leaderboard page
- Profile.unity: the profile page where the users can redeem the tokens earned
- OfflineMap.unity: the users can download offline maps
- Contact.unity: the about page

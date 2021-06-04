# Discord Bot Project
A Discord C#/.NET Core Bot
This Discord bot is a coded in C# with Discord.Net.


## Getting Started

Create a bot account :
    - https://docs.stillu.cc/guides/getting_started/first-bot.html
	- Go to : https://discordapp.com/developers/applications/me
	- Click on "New App", and create one for your bot
	- Then you will have to create a bot user http://imgur.com/opbiwGl
	- Create an invite for your bot using : https://discordapp.com/oauth2/authorize?client_id=APP_ID&scope=bot and replace "APP_ID" with the client ID of your app.

Modify config.json with your bot token and other settings.
Move this file to the same folder as your executable.

Modify startup.cs with route to your database


## Features

    - Post random memes(general or programming)
    - Create a poll that is saved to database
    - Vote for previously made polls
    - Easily add your own commands

## Plans

    - Be able to delete old polls and/or votes
    - Get actual vote results

## How to Use

    - Write "?help" to get started
    - All commands have  "?" as prefix, prefix can be changed in config.json file

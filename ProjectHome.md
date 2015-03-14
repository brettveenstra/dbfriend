## Overview ##
DbFriend produces Sandboxed snapshots of MSSQL databases using Visual Studio solutions.

DbFriend's goal is to take you from zero SQL artifacts to full development environment quickly and easily.


Inspired by the [TreeSurgeon project](http://treesurgeon.codeplex.com), DbFriend's goals is to make it simple to follow good development practices with your SQL server development.


## Quick Start ##
**_Please note:_** DbFriend currently requires MS SQL 2008 SMO and Visual Studio 2008.

  1. Unzip
  1. Run `DbFriendShell.exe`
  1. Supply Server, Database, (Username and Password optional).
  1. Sit back while DbFriend generates a VS 2008 project in your `My Documents\DbFriend\<databasename>` folder.
  1. Find the `setupdb.bat` file in the generated folder and run it.
  1. Tweak the files under the `build\sandbox` folder to properly build up a shell of your database.
  1. Send us Comments and Issues! Much more to come!


## Areas ##
  * [Tutorial](Tutorial.md)
  * [Conventions](Conventions.md)
  * [Assumptions](Assumptions.md)
  * [Roadmap](Roadmap.md)


&lt;wiki:gadget url="http://www.ohloh.net/p/dbfriend/widgets/project\_basic\_stats.xml" height="220" border="1" /&gt;
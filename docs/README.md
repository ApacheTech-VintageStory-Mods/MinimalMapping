# Minimal Mapping

Limited mapping functionality for a more immersive world.

## Suport the Mod Author

If you find this mod useful, and you would like to show appreciation for the work I produce; please consider supporting me, and my work, using one of the methods below. Every single expression of support is most appreciated, and makes it easier to produce updates, and new features for my mods, moving fowards. Thank you.

 - [Join my Patreon!](https://www.patreon.com/ApacheTechSolutions?fan_landing=true)
 - [Donate via PayPal](http://bitly.com/APGDonate)
 - [Buy Me a Coffee](https://www.buymeacoffee.com/Apache)
 - [Subscribe on Twitch.TV](https://twitch.tv/ApacheGamingUK)
 - [Subscribe on YouTube](https://youtube.com/c/ApacheGamingUK)
 - [Purchase from my Amazon Wishlist](http://amzn.eu/7qvKTFu)
 - [Visit my website!](https://apachegaming.net)

## Features:
   
### **Key Bindings** 
    
 - Disabled 'M' Key Binding.
 - Disabled 'V' Key Binding.

### **Patches**

 - Disabled "Show Coordinates HUD" in Settings Menu.
 - Disabled coordinates being shown when hovering over the mini-map.
 - Disabled mini-map scrolling.
 - Mini-map zoom levels now range from 2 to 6, rather than 0.25 to 6.
 - Mini-map zoom level defaults to 3, rather than 2.
 - Waypoints cannot be pinned. I've re-written both the '/waypoint' command, and the Add/Edit Waypoint GUIs. Even works with Waypoint Extensions mod.
 - Coordinates HUD disabled by default, and cannot be enabled.
 - Mini-map visible by default, but can still be manually hidden by player.

## Chat commands

Use the `/minimalmapping` command as the primary entry point. The following sub-commands are available:

<ul class="feature-bullets" style="padding-left: 1rem;">
    <li><b><kbd>/minimalmapping gui</kbd></b> Show the minimal mapping GUI, where all these options can be configured.</li>
    <li><b><kbd>/minimalmapping scope [global|world]</kbd></b> Change whether the mod should use global, or per-world settings.</li>
    <li><b><kbd>/minimalmapping [feature] mode [disabled|enabled|whitelist|blacklist]</kbd></b> Change the mode for the given feature.</li>
    <li><b><kbd>/minimalmapping [feature] whitelist</kbd></b> Show players currently on the feature's whitelist.</li>
    <li><b><kbd>/minimalmapping [feature] whitelist [playerName]</kbd></b> Add or remove a player from the feature's whitelist.</li>
    <li><b><kbd>/minimalmapping [feature] blacklist</kbd></b> Show players currently on the feature's blacklist.</li>
    <li><b><kbd>/minimalmapping [feature] blacklist [playerName]</kbd></b> Add or remove a player from the feature's blacklist.</li>
    <li><b><kbd>/minimalmapping worldmap worldmap-hotkey [on|off]</kbd></b> Disable or enable the 'M' hotkey for the world map.</li>
    <li><b><kbd>/minimalmapping worldmap minimap-hotkey [on|off]</kbd></b> Disable or enable the mini-map hotkey (F6).</li>
    <li><b><kbd>/minimalmapping worldmap wipe-at-login [on|off]</kbd></b> Wipe the player's world map whenever they log in.</li>
    <li><b><kbd>/minimalmapping worldmap wipe-on-death [on|off]</kbd></b> Wipe the player's world map whenever they die.</li>
    <li><b><kbd>/minimalmapping worldmap coordinates-hud [on|off]</kbd></b> Allow or disallow the on-screen coordinates HUD for players.</li>
    <li><b><kbd>/minimalmapping worldmap coordinates-hover [on|off]</kbd></b> Enable or disable showing coordinates when hovering over the mini-map.</li>
    <li><b><kbd>/minimalmapping worldmap minimap-scroll [on|off]</kbd></b> Enable or disable mini-map scrolling.</li>
    <li><b><kbd>/minimalmapping worldmap zoom-min [0.125-12]</kbd></b> Set the minimum mini-map zoom level.</li>
    <li><b><kbd>/minimalmapping worldmap zoom-default [0.25-6]</kbd></b> Set the default mini-map zoom level.</li>
    <li><b><kbd>/minimalmapping worldmap zoom-max [0.125-12]</kbd></b> Set the maximum mini-map zoom level.</li>
    <li><b><kbd>/minimalmapping worldmap force-minimap [on|off]</kbd></b> Force the mini-map to be active by default.</li>
    <li><b><kbd>/minimalmapping waypoints pins [on|off]</kbd></b> Allow or disallow pinning of waypoints.</li>
    <li><b><kbd>/minimalmapping waypoints wipe-at-login [on|off]</kbd></b> Wipe a player's waypoints when they log in.</li>
    <li><b><kbd>/minimalmapping waypoints wipe-on-death [on|off]</kbd></b> Wipe a player's waypoints when they die.</li>
</ul>
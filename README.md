# ShigarakiModRor2
Shigaraki Tomura mod for Ror2
Go Beyond, Plus Chaos!
## Shigaraki
Adds Shigaraki from My Hero Academia, a multi-option survivor steal 'quirks' from enemies, gaining their abilites/skills.
https://thunderstore.io/package/TeaL/ShigarakiMod/
#### Multiplayer works (hopefully). CustomEmotesAPI compatibility. Risk of Options support.
#### Message me on the Risk of Rain 2 Modding Discord if there are any issues- teal5571.
#### <a href="https://ko-fi.com/tealpopcorn"><img src="https://user-images.githubusercontent.com/93917577/160220529-efed5020-90ac-467e-98f2-27b5c162d744.png"> </a>
If you enjoy my work, support me on Ko-fi!
## Popcorn Factory
<details>
<summary>Check out other mods from the Popcorn Factory team!</summary>	

<div>
    <a href="https://thunderstore.io/package/PopcornFactory/Arsonist_Mod/">
      <img width="130" src="https://github.com/user-attachments/assets/5928595d-ac4a-4bb7-9ef2-1e56d74ccb7d"/>
      <p>Arsonist Mod (Popcorn Factory Team)</p>
    </a>
</div>	
<div>
    <a href="https://thunderstore.io/package/PopcornFactory/Rimuru_Tempest_Mod/">
      <img width="130" src="https://github.com/user-attachments/assets/7ca86047-1bbb-4b2c-8b98-3cb6f65f86b3"/>
      <p>Rimuru Tempest Mod (Popcorn Factory Team)</p>
    </a>
</div>
<div>
    <a href="https://thunderstore.io/package/PopcornFactory/DarthVaderMod/">
      <img width="130" src="https://user-images.githubusercontent.com/93917577/180753359-4906ca0b-6ce5-4ff7-9962-bdec3329682c.png"/>
      <p>Darth Vader Mod (Popcorn Factory Team)</p>
    </a>
</div>
<div>
    <a href="https://thunderstore.io/package/PopcornFactory/DittoMod/">
        <img src="https://user-images.githubusercontent.com/93917577/168004690-23b6d040-5f89-4b62-916b-c40d774bff02.png"><br>
        <p>DittoMod (My other Mod!)</p>
    </a>
</div>
<div>
    <a href="https://thunderstore.io/package/TeaL/DekuMod/">
        <img width ="130" src="https://github.com/user-attachments/assets/117f7100-e25d-4d34-8811-71d4fdf94c61" ><br>
        <p>DekuMod (My other Mod!)</p>
    </a>
</div>
<div>
    <a href="https://thunderstore.io/package/TeaL/NoctisMod">
      <img width="130" src="https://github.com/user-attachments/assets/e5ef6c35-f487-46f0-b6e7-aab58afd2a60"/>
      <p>Noctis Mod (My other Mod!)</p>
    </a>
</div>
<div>
    <a href="https://thunderstore.io/package/Ethanol10/Ganondorf_Mod/">
        <img src="https://github.com/user-attachments/assets/79f2ce62-04a0-4f89-a4e9-13d351401f37"><br>
        <p>Ganondorf Mod (Ethanol 10)</p>
    </a>
</div>
<div>
    <a href="https://thunderstore.io/package/BokChoyWithSoy/Phoenix_Wright_Mod/">
        <img src="https://github.com/user-attachments/assets/74f85c95-5ae5-4017-af33-afbbbafc868f"><br>
        <p>Phoenix Wright Mod (BokChoyWithSoy)</p>
    </a>
</div>
<div>
    <a href="https://thunderstore.io/package/PopcornFactory/Wisp_WarframeSurvivorMod/">
        <img width ="130" src="https://github.com/user-attachments/assets/5cde736a-4c63-4a8f-84f7-72c787cf40b4" ><br>
        <p>Wisp Mod (Popcorn Factory Team)</p>
    </a>
</div>
</details>

## Latest Changelog, Next update(s)

- 3.0.2- Additional features
    - Made the game pause when opening the menu (in single player only)
    - Added more configs for a bunch of quirks
- 3.0.1- Hotfix
    - Fixed skills that could be held down to continue their use not doing that
    - Fixed configs for Decay not being properly used
    - Fixed some names of skills
    - Added his base skills to be all selectable from the loadout- note that you can also add them during a run at anytime
- 3.0.0- Major Update- The Quirk Rework!
    - New Model and animations for Shigaraki, based off his Season 5 look
    - ### Reworked All For One System
  		- Stealing functionality remains the same, however now he is able to store Quirks!
   		- Open the new Quirk menu with V (configurable), and you will have a drop-down box to select what quirk you would like for each skill slot. This allows the amassing of quirks over the run, enabling easier combinations of quirks (previously would require you to have it on your skill slot), resulting in the ability to freely create and alter your build at any point!
    	- Under the hood, the way quirks are registered have changed- what this means is that I'm now able to freely take any combination of quirks to craft a new quirk, no longer requiring quirks to only be paired. Some of the new skill additions in this update will utilise this and this means some quirks can have multiple 'recipes', allowing an infinite number of new quirk combinations.
    	- This does come with some balance changes- mainly that stealing always costs 1/2 your plus chaos (is affected by purity or alien heads for discounts), as well as a limit on the range of All For One- normally it's 10 meters but it can be configured to the max of 70 meters, similar to his current iteration. 
    - ### New Mechanic- Apex Adaptation System
    	- To mimic the surgery that Shigaraki went through in the series, a new system has been implemented.
     	- By holding down the respective skill slot, you can RESET that skills cooldown.
      	- By resetting the skill- 2 things happen
      	- <img width="128" height="128" alt="image" src="https://github.com/user-attachments/assets/e285879c-1fc3-4cd3-a988-86d79f889e8d" />
      	- You gain **Apex** stacks per second of skill reset- indicated by the inner circle of this UI.
      		- These apply a negative debuff to your health regen, and at it's limit will result in a **Quirk Overdrive**, making you take damage and be unable to act as well as unable to heal for 4 seconds.
      	- You gain **adaptation** stacks per second of skill reset- indicated by the outer ring of this UI.
      		- This stacks throughout the run and as you hit the threshold of 100 stacks (100 seconds of resetting), you gain permanent bonuses to damage, armor and movespeed, as well as increasing your apex stack limit.
      	 - These 2 mechanics were done in order to give Shigaraki that feeling of being able to push his body to it's limits and use his quirks (skills) more, however his body can only handle so much, thus the apex stacks. Over time he gets used to his body and is able to better control and adapt to it.
    - ### Balance changes
    	- As mentioned before, Shigaraki has a lot more power due to the ability to easily optimise your build at any point in the run. Along with this, some skills have been udpated due to the ability to reset.
     	- Shigaraki's **base damage** 5 -> 1. Still gains 1 damage per level. I know- this seems ridiculous but this mostly matters with Shigaraki's early-mid game due to the ease of gaining quirks and bufffs, his damage had to be toned down. Quirks like the Strength - Beetle passive still give him 5 damage and are there to still help his early game.
      	- **Omniboost** - damage and attack speed 1.3x -> 1.2x, every 3rd hit gain 5% -> 4% damage and attack speed. Lose half the stacks when hitting a different enemy. Omniboost was a very powerful multiplier. The build-up of it is very powerful now especially since skills can be reset. It's now been balanced to halve stacks when hitting a different enemy, similar to when killing one.
      	- **Refresh** - Cooldown increased from 20 to 60 seconds. This skill previously reset ALL skills' cooldowns as well as refund half of your total energy. Being easily accessible at 20 seconds for the value of all 8 skillslots (not including some passive skills) resetting was too powerful.
    - ### New Quirks
    	- CHEF - **Oil Burst Passive**- Every 6 attacks, shoot a glob of oil.
      	- Drifter - **Salvage**- Create temporary items, spending 20 plus chaos per item, up until 10.
     	- Operator - **S1-41 Custom**- Shoot, dealing 600% damage and ricocheting on kill.
     	- False Son - **Stolen Inheritance Passive**- gain 1% of max HP as additional base damage.
     	- Seeker - **Meditate**- Give everyone around you 25% of their max HP as barrier.
      	- Scorch Worm - **Lava Bomb**- Shoot a lava bomb, dealing 200% damage.
      	- Halcyonite - **Halcyonite's Greed**- Spend your money. Gain a buff based on how much money is spent, draining over time. (?)
      	-  Solus Distributor - **Solus Plant Mine**- Summon 6 mines. Each can apply the **Primed debuff**- a new debuff that causes enemies to take 10% bonus damage per stack, reducing by 1 per hit.
      	-  Solus Extractor - **Solus Extract**- Dash and stab, bouncing off the first enemy hit. Apply the **Primed debuff**- a new debuff that causes enemies to take 10% bonus damage per stack, reducing by 1 per hit. Hitting a Primed enemy reduces the cooldowns of all skills based on how many stacks they have.
      	-  Solus Invalidator - **Solus Invalidate**- Stun Target and enemies around. Apply the **Primed debuff**- a new debuff that causes enemies to take 10% bonus damage per stack, reducing by 1 per hit. Targetting a Primed enemy freezes them instead.
      	-  Solus Prospector - **Solus Priming Passive**- Attacks apply the primed debuff.
      	-  Solus Scorcher - **Solus Accelerate**- Apply accelerant to Target and enemies around. Apply the **Primed debuff**- a new debuff that causes enemies to take 10% bonus damage per stack, reducing by 1 per hit. Targetting a Primed enemy also burns them.
      	-  Solus Transporter - **Solus Transport**- Teleport to Target. Apply the **Primed debuff**- a new debuff that causes enemies to take 10% bonus damage per stack, reducing by 1 per hit. Targetting a Primed enemy resets this specific skill's cooldown.
		-  Alloy Hunter - **Crit Boost Passive**- Critical hits deal 2x damage.
      	-  Solus Amalgamator - **Equipment Boost Passive**- Every hit reduces equipment cooldowns by 0.5 seconds.
      	-  **Solus Factor Unleashed** NEW combined skill requiring all 6 Solus enemies' quirks to be owned- Spend all plus chaos. For every 10 plus chaos, gain 1 second of the **Super Primed Buff**- a new debuff that makes it so you no longer reduce the stacks of the primed debuff, instead increasing them if the enemy is primed as well as causing a blast attack, applying primed to all enemies hit.
      	-  **Hyper Regeneration Passive** NEW combined skill requiring Jellyfish, Mini Mushrum, False Son and Seeker- Heal 10% of hp every second.
		
    - ### Changes
    	- Animation timing improvements across the board to match with the skills, more specific animations for each skill
     	- Icons for base skills now solely show the specific monster/survivor with a background that either indicates they are an active or a passive skill.
      	- **Airwalk** is now a proper flight mechanic- when you double tap jump you activate airwalk, allowing you to freely fly horizontally. Hold the jump button to rise and double tap jump to deactivate it. Furthermore, sprinting will result in you heading towards where you're looking at.
      	- Additional configs- all risk of options support- for everything with the apex adapt. Configs for some mechanics as well like decay, and some other quirks. Plans to add more in the future.

- Next update(s)
    - More Quirks! either more from the series itself or from anything.
    - Changing/adding new models/skins
    - Bug fixes. 
    - More/Improved animations.
    - Update Miro/Make a Wiki for all his abilities
    - Aim to do more tutorials/guides on explaining Shigaraki?

<img src= "https://github.com/user-attachments/assets/1dbefd26-8eaf-4c47-9a2e-3ff739613af9" height ="256" >
<img src= "https://github.com/user-attachments/assets/bf843ff3-8f62-4fb5-8c77-00b35c9cc271" height ="256" >
<img src= "https://github.com/user-attachments/assets/48485919-aeec-4904-bf72-44a62d1fe48d" height ="256" >
<img src= "https://github.com/user-attachments/assets/0c627cec-a39e-4a77-a9e8-11600461f661" height ="256" >
<img src= "https://github.com/user-attachments/assets/a28807f5-6b61-4d33-be9c-96bb20d6a5bb" height ="256" >

## Known Issues
    
## Overview
    He is initially weak with low base damage, aim to get passive quirks to increase his power.
    All For One can grab elite equipment from elites. 
    Different quirks builds his playstyle, such as focusing on procs, high damage, buffs, debuffs, defensive, mobility, utility, it's up to you.
	You can reset your skills by holding the same skill slot button, this comes at a cost but over the run will also grant permanent stats. 
    It's good to have some base skills too as synergy and ultimate skills aren't usually direct upgrades.
    Beetle Queen's Summon Ally quirk allows you to get the base survivor's summoned and therefore be able to steal their quirks too. 
    https://miro.com/app/board/uXjVPKipCPA=/?share_link_id=782257375770 This is a Miro link to a visual of all the skills (Outdated- plans to update)

## Skills
### Passive
Shigaraki has a plus chaos meter- this is used for a few skills and his main quirk- All For One. It regenerates over time and every kill gives 10% of your max plus chaos. 

Shigaraki can grab the target's quirk with All For One by pressing the F key by default. (Costs half of your max plus ultra by default). 
Open the quirk menu by pressing the V key by default. 
Shigaraki can give passive quirks to enemies by pressing the C key by default, then pressing a skill slot that contains a passive(Costs 50 plus ultra by default).

Shigaraki has Air Walk: Double tapping jump activates Air Walk. This enables you to float and move around horizontally in the air. Hold jump to rise, double tap jump to deactivate. Sprinting makes you move towards where you're looking at. Drains 5 plus chaos every second.

All [Melee]/Overlap attacks attacks Decay, which decreases the enemies' movespeed and attackspeed as well as instakilling them at 100 stacks (except for Teleporter Bosses).

Shigaraki can sprint in any direction and has a double jump.

### Base Skills

<details>
<summary>Click to expand base skills</summary>	
<table>
<thead>
  <tr>
    <th>Quirk</th>
    <th>Icon</th>
    <th>Description</th>
  </tr>
</thead>
<tbody>
  <tr>
    <td>Passives</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/Base/allforone.png?raw=true"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/07984117-9535-4e63-af1b-7b5e1367a3b3"></td>
  </tr>
  <tr>
    <td>All For One</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/Base/allforone.png?raw=true" alt="Image"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/94c409bd-fe00-4e02-bbcb-ac8c4bdf5e13"></td>
  </tr>
  <tr>
    <td>Decay</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/BaseSkills/decay.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/6e05d78c-c471-4c59-ad86-c248db977fa4"></td>
  </tr>
  <tr>
    <td>Bullet Laser<br></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/BaseSkills/bulletlaser.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/cff3ea37-9def-469b-9de0-32d3b84e4d11"></td>
  </tr>
  <tr>
    <td>Air Cannon</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/BaseSkills/aircannon.PNG"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/28fc6fea-c70c-4d9f-b28b-bcf1c767ccbd"></td>
  </tr>
  <tr>
    <td>Multiplier</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/BaseSkills/multiplier.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/672195fb-94ca-45af-a3e4-850234f3e560"></td>
  </tr>
</tbody>
</table>
</details>

### Quirk Passives

<details>
<summary>Click to expand base passives</summary>	
<table>
<thead>
  <tr>
    <th>Quirk</th>
    <th>Icon</th>
    <th>Description</th>
  </tr>
</thead>
<tbody>
  <tr>
    <td>Alpha Construct - Barrier</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Alpha_Construct.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/e368b22b-7e56-442f-92fa-1b67b88fd2d3"></td>
  </tr>
  <tr>
    <td>Beetle - Strength Boost</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Beetle.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/49158768-506d-4342-b881-e5861f87e76a"></td>
  </tr>
  <tr>
    <td>Blind Pest - Jump Boost</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Blind_Pest.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/17c595f6-7ee2-4b9a-b8ef-468356161cc1"></td>
  </tr>
  <tr>
    <td>Blind Vermin - Super Speed</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Blind_Vermin.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/090d64e4-9cd2-41f0-b2e4-bcaf831913ed"></td>
  </tr>
  <tr>
    <td>Gup/Geep/Gip - Spiky Body</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Gup.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/e77b247f-9f32-4838-81d3-346c33026126"></td>
  </tr>
  <tr>
    <td>Hermit Crab - Mortar</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Hermit_Crab.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/299399c2-b462-4b08-8000-0c40b0370153"></td>
  </tr>
  <tr>
    <td>Larva - Acid Jump</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Larva.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/ac4054ec-035b-4c26-ad00-c0bbfa0e1fba"></td>
  </tr>
  <tr>
    <td>Lesser Wisp - Haste</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Lesser_Wisp.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/a6f69af8-378b-4a6e-ac3d-9013e57c6268"></td>
  </tr>
  <tr>
    <td>Lunar Exploder - Lunar Barrier</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Lunar_Exploder.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/d02e76d6-9b4e-4489-a831-1127dafcf00f"></td>
  </tr>
  <tr>
    <td>Mini Mushrum - Healing Aura</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Mini_Mushrum.png"></td>
    <td><img src="https://github.com/user-attachments/assets/f8aa2d8e-2a20-4d82-85a5-72018a524661"></td>
  </tr>
  <tr>
    <td>Solus Probe - Solus Boost</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Solus_Probe.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/a452f7b4-7f8e-44fa-ade5-93c3b2903baf"></td>
  </tr>
	  <tr>
    <td>Solus Prospector - Solus Priming</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/SolusProspector.png"></td>
    <td><img src="https://github.com/user-attachments/assets/57340141-b3ee-474e-b744-5244ddaa9aa5"></td>
  </tr>
  <tr>
    <td>Void Barnacle - Void Mortar</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Void_Barnacle.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/34ae7614-a775-4ca2-9dbc-6f8824f99558"></td>
  </tr>
  <tr>
    <td>Void Jailer - Gravity</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Void_Jailer.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/e92eb812-a12a-43dd-b762-b673e7095bb6"></td>
  </tr>
  <tr>
    <td>Alloy Hunter - Crit Boost</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Alloy_Hunter.png"></td>
    <td><img src="https://github.com/user-attachments/assets/c0797cf6-4e46-42d5-a81c-a83df3ea873f"></td>
  </tr>
  <tr>
    <td>Imp Overlord - Bleed</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Imp_Overlord.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/f4a90506-e103-4d54-b31a-09c296be1a22"></td>
  </tr>
  <tr>
    <td>Magma Worm - Blazing Aura</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Magma_Worm.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/650ca0ec-0bd1-4a17-8c72-d57cec64352c"></td>
  </tr>
  <tr>
    <td>Overloading Worm - Lightning Aura</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Overloading_Worm.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/3c71f9ab-a87e-4244-97f2-b13e76e91c36"></td>
  </tr>
  <tr>
    <td>Solus Amalgamator - Equipment Boost</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Solus_Amalgamator.png"></td>
    <td><img src="https://github.com/user-attachments/assets/5895fef2-40c4-45e8-a723-63c203dd6bec"></td>
  </tr>
  <tr>
    <td>Stone Titan - Stone Skin</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Stone_Titan.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/f1dd6ae4-08a9-44bb-aecd-325d6fc2ff6e"></td>
  </tr>
  <tr>
    <td>Wandering Vagrant - Vagrant's Orb</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Wandering_Vagrant.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/40bbe57f-736d-4614-a117-b97bd790d392"></td>
  </tr>
  <tr>
    <td>Acrid - Poison</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Acrid.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/f42e4034-191a-426c-b884-2fa7f4f0b37e"></td>
  </tr>
  <tr>
    <td>Captain Defensive Microbots</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Captain.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/d2e22b41-f977-4574-8d0b-3707cc4b7ea8"></td>
  </tr>
  <tr>
    <td>CHEF - Oil Burst</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/CHEF.png"></td>
    <td><img src="https://github.com/user-attachments/assets/4451ea4e-9c20-49b3-97c3-97ee36614a41"></td>
  </tr>
  <tr>
    <td>Commando - Double Tap</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Commando.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/9542d230-035b-4a3b-942a-ddbb79c648d9"></td>
  </tr>
	<tr>
    <td>False Son - Stolen Inheritance</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/False_Son.png"></td>
    <td><img src="https://github.com/user-attachments/assets/8e77c42b-c78d-43b0-8dbe-707ba34039a5"></td>
  </tr>
  <tr>
    <td>Loader - Scrap Barrier</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Loader.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/de311eca-97b2-4fd9-8779-7b35b028a567)"></td>
  </tr>
</tbody>
</table>
</details>

### Combined Passives

<details>
<summary>Click to expand combined passives</summary>	
<table>
<thead>
  <tr>
    <th>Quirk</th>
    <th>Icon</th>
    <th>Description</th>
  </tr>
</thead>
<tbody>
  <tr>
    <td>Big Bang</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/BigBang.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/90f108ea-4c51-4095-a95c-fb7a10f28f45"></td>
  </tr>
  <tr>
    <td>Wisper</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/Wisper.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/0a35bbdc-3250-4aee-aa91-e34a49065dda"></td>
  </tr>
  <tr>
    <td>Omniboost</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/Omniboost.png" width="256" ></td>
    <td><img src="https://github.com/user-attachments/assets/75de7000-5a50-4898-974b-5295ed469caa"></td>
  </tr>
  <tr>
    <td>Gacha</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/Gacha.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/0d6d323e-747a-4b0e-8412-d16ea9e0da94"></td>
  </tr>
  <tr>
    <td>Stone Form</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/StoneForm.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/8e3186fc-58d1-40b7-913a-04af350a1a19"></td>
  </tr>
  <tr>
    <td>Aura Of Blight</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/AuraOfBlight.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/d8f4f096-258e-4ae7-9e23-392c699c8358"></td>
  </tr>
  <tr>
    <td>Barbed Spikes</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/BarbedSpikes.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/0046f909-42f2-41f8-abc5-f396ee14e78a"></td>
  </tr>
  <tr>
    <td>Ingrain</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/Ingrain.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/910877c4-fd5f-4390-bc61-ada92cb4ed59"></td>
  </tr>
  <tr>
    <td>Double Time</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/DoubleTime.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/90df831f-eb72-4004-8d0d-72953d2df24f"></td>
  </tr>
  <tr>
    <td>Blind Senses</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/BlindSenses.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/5fb63eea-00d9-4599-ac94-4b121e662b7d"></td>
  </tr>
	<tr>
    <td>Supernova</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/Supernova.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/4fe69668-9879-4c44-9767-6e9078e4ce2e"></td>
  </tr>
  <tr>
    <td>Reversal</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/Reversal.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/c7102544-c459-4ed3-9c1b-142f1f50edc5"></td>
  </tr>
  <tr>
    <td>Machine Form</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/MachineForm.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/1ee8e45e-8f4b-4466-aec5-b5551efe731d"></td>
  </tr>
  <tr>
    <td>Gargoyle Protection</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/GargoyleProtection.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/cbef67d4-6dcc-4df2-9f9b-839c10f8990f"></td>
  </tr>
  <tr>
    <td>Weather Report</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/WeatherReport.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/fbc401a3-ebff-4e2c-93dd-9f85b94d914f"></td>
  </tr>
  <tr>
    <td>Decay Awakened</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/DecayAwakened.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/88c94925-7b16-4966-a0d8-0c4b8285e8b0"></td>
  </tr>
  <tr>
    <td>Hyper Regeneration</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/HyperRegeneration.png" width="256" ></td>
    <td><img src="https://github.com/user-attachments/assets/5103971f-fa17-4825-9754-dadab80d0fff"></td>
  </tr>
</tbody>
</table>
</details>


### Quirk Actives

<details>
<summary>Click to expand base actives</summary>	
<table>
<thead>
  <tr>
    <th>Quirk</th>
    <th>Icon</th>
    <th>Description</th>
  </tr>
</thead>
<tbody>
  <tr>
    <td>Alloy Vulture - Wind Blast</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Alloy_Vulture.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/1bcc8647-b2b1-4098-a088-85e3323fe870"></td>
  </tr>
  <tr>
    <td>Beetle Guard - Fast Drop</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Beetle_Guard.png" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/006823bd-9f3e-4325-b2dc-7fd783323626"></td>
  </tr>
  <tr>
    <td>Bighorn Bison - Charging</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Bighorn_Bison.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/a1a61e45-ab3e-4753-8efe-1013ee1085f2"></td>
  </tr>
  <tr>
    <td>Brass Contraption - Spiked Ball Control<br></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Brass_Contraption.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/33809b62-4ede-4967-a16f-a949be6790b1"></td>
  </tr>
  <tr>
    <td>Clay Apothecary - Clay AirStrike</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Clay_Apothecary.png"></td>
    <td><img src="https://github.com/user-attachments/assets/d01ed3f6-de5e-4a3e-a872-91271283212b"></td>
  </tr>
  <tr>
    <td>Clay Templar - Clay Minigun</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Clay_Templar.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/66a3f815-838f-4d71-8a6b-861bfd3f43b7"></td>
  </tr>
  <tr>
    <td>Elder Lemurian - Fire Blast</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Elder_Lemurian.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/3e7c2b0b-8b60-46bb-9a88-63f72536166e"></td>
  </tr>
  <tr>
  <tr>
    <td>Greater Wisp - Spirit Boost</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Greater_Wisp.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/1d326440-c26d-4081-a8f9-050f36cdc1fc"></td>
  </tr>
  <tr>
    <td>Imp - Blink</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Imp.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/af278b95-3a84-4e49-bb73-1e62b0cfb7d0"></td>
  </tr>
  <tr>
    <td>Jellyfish - Regenerate</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Jellyfish.png"></td>
    <td><img src="https://github.com/user-attachments/assets/63cdee5b-a4f5-46cd-836c-55ac0004c740"></td>
  </tr>
  <tr>
    <td>Lemurian - Fireball</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Lemurian.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/d52879af-40be-405c-881b-aab3c3e711b7"></td>
  </tr>
  <tr>
    <td>Lunar Golem - Slide Reset</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Lunar_Golem.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/f8f2d6fe-9075-4e3d-b536-f4900d2c341a"></td>
  </tr>
  <tr>
    <td>Lunar Wisp - Lunar Minigun</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Lunar_Wisp.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/9d262f32-66fd-4ad3-a178-349423f25544"></td>
  </tr>
  <tr>
    <td>Parent - Teleport</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Parent.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/edb227f5-ae8c-4bdd-9220-ba0bb714af10"></td>
  </tr>
  <tr>
    <td>Solus Distributor - Solus Plant Mine</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Solus_Distributor.png"></td>
    <td><img src="https://github.com/user-attachments/assets/7b59ce02-1f8d-4901-ad45-247d14ec6108"></td>
  </tr>
  <tr>
    <td>Solus Extractor - Solus Extract</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Solus_Extractor.png"></td>
    <td><img src="https://github.com/user-attachments/assets/cb341dcf-cfb3-48f5-8373-5407dd0d1bd7"></td>
  </tr>
  <tr>
    <td>Solus Invalidator - Solus Invalidate</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Solus_Invalidator.png"></td>
    <td><img src="https://github.com/user-attachments/assets/8fb08d47-017e-420b-a0e5-8e70249f09bd"></td>
  </tr>
  <tr>
    <td>Solus Scorcer - Solus Accelerate</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Solus_Scorcher.png"></td>
    <td><img src="https://github.com/user-attachments/assets/9cb23bef-38d3-4b9a-bd57-c482def25a10"></td>
  </tr>
  <tr>
    <td>Solus Transporter - Solus Transport</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Solus_Transporter.png"></td>
    <td><img src="https://github.com/user-attachments/assets/8dcddd61-d5f0-4eb5-883a-9c3cd90c794e"></td>
  </tr>
  <tr>
    <td>Stone Golem - Laser</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Stone_Golem.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/bd652eb3-ce3e-4c26-a7c7-d785d59b8cf5"></td>
  </tr>
  <tr>
    <td>Void Reaver - Nullifier Artillery</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Void_Reaver.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/1744df7f-5e82-4bea-aec2-81567f8a78cb"></td>
  </tr>
  <tr>
    <td>Beetle Queen - Summon Ally</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Beetle_Queen.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/e8b4ff1b-d823-43a4-bb83-a9761a9aaede"></td>
  </tr>
  <tr>
    <td>Grandparent - Solar Flare</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Grandparent.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/577dbae0-2895-4e87-839d-21239e2d84ea"></td>
  </tr>
  <tr>
    <td>Grovetender - Chain</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Grovetender.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/6819cf3d-48e0-45b5-a3d9-621cbfacad30"></td>
  </tr>
  <tr>
    <td>Clay Dunestrider - Tar Boost</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Clay_Dunestrider.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/06464c70-b7fe-4365-9745-8cb740b3c7fd"></td>
  </tr>
  <tr>
    <td>Scavenger - Throw Thqwibs</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Scavenger.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/7598bd7e-8794-441d-9ecb-9ff4526f29d5"></td>
  </tr>
  <tr>
    <td>Solus Control Unit - Anti Gravity</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Solus_Control_Unit.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/e59a836e-091a-4eac-87e0-017910ce3c4b"></td>
  </tr>
  <tr>
    <td>Void Devastator - Void Missiles</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Void_Devastator.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/47e96504-b2e3-47e8-a853-377e27a0fe54"></td>
  </tr>
  <tr>
    <td>Xi Construct - Beam</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Xi_Construct.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/62f8bc78-97c5-403b-8bed-4d937a1857b5"></td>
  </tr>
  <tr>
    <td>Artificer - Elementality: Fire</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/BaseSkills/ArtificerFire.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/95f99bf3-77a5-46a4-ac03-64a564e84642"></td>
  </tr>
  <tr>
    <td>Artificer - Elementality: Ice</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/BaseSkills/ArtificerIce.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/1c83e245-d02e-4c2f-af11-e495a7a0a5e8"></td>
  </tr>
  <tr>
    <td>Artificer - Elementality: Lightning</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/BaseSkills/ArtificerLightning.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/f9289f1b-7782-4d17-ac9c-3776f57ab8cb"></td>
  </tr>
  <tr>
    <td>Bandit - Lights Out</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Bandit.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/f7514ed6-265d-454e-9f82-d01a65a98723"></td>
  </tr>
  <tr>
    <td>Engineer - Turret</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Engineer.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/4add3d58-dfcd-416d-bd31-0723974323a4"></td>
  </tr>
  <tr>
    <td>Huntress - Flurry</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Huntress.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/b412f6c7-3b64-497c-92e5-0a89fec7dec5"></td>
  </tr>
  <tr>
    <td>Mercenary - Wind Assault</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Mercenary.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/bb00a919-2fb8-4f00-a66a-9e2b7b626345"></td>
  </tr>
  <tr>
    <td>MUL-T - Power Stance</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/MUL-T.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/bb77e8c1-defb-4fbe-8e79-05fb6fe51811"></td>
  </tr>
  <tr>
    <td>Railgunner - Cryocharged Railgun</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Railgunner.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/1c242da8-f6cf-40b2-8050-d843da9896bc"></td>
  </tr>
  <tr>
    <td>REX - Seed Barrage</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/REX.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/29c939e9-0c2f-44e0-8bf0-90b97855e728"></td>
  </tr>
  <tr>
    <td>Void Fiend - Cleanse</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/EnemyIcons/Void_Fiend.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/7fe0ec45-7366-42f5-a1e8-faf36a0b8eb6"></td>
  </tr>
  <tr>
    <td>One For All</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/BaseSkills/OFA.png"></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/788ecfb5-eb24-4b06-845f-3bc974e70fb7"></td>
  </tr>
</tbody>
</table>
</details>


### Combined Actives

<details>
<summary>Click to expand combined actives</summary>	
<table>
<thead>
  <tr>
    <th>Quirk</th>
    <th>Icon</th>
    <th>Description</th>
  </tr>
</thead>
<tbody>
  <tr>
    <td>Sweeping Beam</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/SweepingBeam.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/424a8f36-9f47-4ba6-9691-45e573e96dc3"></td>
  </tr>
  <tr>
    <td>Blackhole Glaive</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/BlackholeGlaive.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/473fa1e0-db6c-41fb-9c26-91ac3d8912ac"></td>
  </tr>
  <tr>
    <td>Gravitational Downforce</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/GravitationalDownforce.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/e4666ccf-fd54-4107-9a76-243a5236ca61"></td>
  </tr>
  <tr>
    <td>Wind Shield</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/WindShield.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/eb33d7ed-ed36-44e5-acd2-e454c562906b"></td>
  </tr>
  <tr>
    <td>Genesis</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/Genesis.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/4be012f2-e65f-4504-b2ef-67ccddabdd55"></td>
  </tr>
  <tr>
    <td>Refresh</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/Refresh.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/d565a767-7444-4a44-beae-e39c42054f71"></td>
  </tr>
  <tr>
    <td>Expunge</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/Expunge.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/fbf13ead-425b-4c0d-a34c-ded12e95d940"></td>
  </tr>
  <tr>
    <td>Shadow Claw</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/ShadowClaw.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/4aa46278-cd6a-49a6-8aed-99673d6b4c03"></td>
  </tr>
  <tr>
    <td>Orbital Strike</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/OrbitalStrike.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/391e5457-9af1-424f-85c5-dbd3ca1fb074"></td>
  </tr>
  <tr>
    <td>Thunderclap</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/ThunderClap.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/efd380ad-114f-4b6a-aef3-225262bcf918"></td>
  </tr>
  <tr>
    <td>Blast Burn</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/BlastBurn.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/ad51135f-3ce7-408e-a5c7-8aceb972aa46"></td>
  </tr>
  <tr>
    <td>Barrier Jelly</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/BarrierJelly.jpg" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/3ac9b83b-8bf2-4aab-877e-8c4aa4ef0a3b"></td>
  </tr>
  <tr>
    <td>Mech Stance</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/MechStance.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/ab750ef6-8a05-45f8-87c0-887e1e71e892"></td>
  </tr>
  <tr>
    <td>Wind Slash</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/WindSlash.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/bd133143-1f70-4df4-808f-3a940ad32a27"></td>
  </tr>
  <tr>
    <td>Limit Break</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/LimitBreak.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/117072cd-7383-486e-b10c-f73bdbcf6459"></td>
  </tr>
  <tr>
    <td>Void Form</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/VoidForm.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/6b1bd344-fe06-495f-aaba-9c6d4ea3ce6c"></td>
  </tr>
  <tr>
    <td>Elemental Fusion</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/ElementalFusion.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/d2719c5e-d245-4b01-a97a-d168d5b41dfa"></td>
  </tr>
  <tr>
    <td>Decay Plus Ultra</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/DecayPlusUltra.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/31b75bb8-69ea-4b06-8560-e421593e04a4"></td>
  </tr>
  <tr>
    <td>Mach Punch</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/MachPunch.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/65ca0675-f7b9-419b-befe-d5c45e474685"></td>
  </tr>
  <tr>
    <td>Rapid Pierce</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/RapidPierce.png" width="256" ></td>
    <td><img src="https://github.com/user-attachments/assets/89565371-fa6f-435d-addf-49738cdcf835"></td>
  </tr>
  <tr>
    <td>The World</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/TheWorld.png" width="256" ></td>
    <td><img src="https://github.com/user-attachments/assets/0e13058d-acfc-4b75-98ab-65e468e98b8d"></td>
  </tr>
  <tr>
    <td>Extreme Speed</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/ExtremeSpeed.png" width="256" ></td>
    <td><img src="https://github.com/user-attachments/assets/9a013622-b67b-4bca-ae20-5307076c9fe0"></td>
  </tr>
  <tr>
    <td>Death Aura</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/DeathAura.png" width="256" ></td>
    <td><img src="https://github.com/1TeaL/Shigaraki/assets/93917577/cdc8ed14-bfa6-4ee0-a044-a51ee29eeef7"></td>
  </tr>
  <tr>
    <td>One For All For One</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/OneForAllForOne.png" width="256" ></td>
    <td><img src="https://github.com/user-attachments/assets/978c18fd-e427-4581-bcbc-bed90f765bba"></td>
  </tr>
  <tr>
    <td>X Beamer</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/XBeamer.png" width="256" ></td>
    <td><img src="https://github.com/user-attachments/assets/ef12d3ed-c860-4ee0-bd19-11cf6ed75b84"></td>
  </tr>
  <tr>
    <td>Final Release</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/FinalRelease.png" width="256" ></td>
    <td><img src="https://github.com/user-attachments/assets/7039e330-f7de-4db9-9971-b418e605d3e9"></td>
  </tr>
  <tr>
    <td>Blasting Zone</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/BlastingZone.png" width="256" ></td>
    <td><img src="https://github.com/user-attachments/assets/3660855e-6285-4c26-bf21-61643610ab13"></td>
  </tr>
  <tr>
    <td>Wild Card</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/WildCard.png" width="256" ></td>
    <td><img src="https://github.com/user-attachments/assets/171f790b-9972-420a-a2b8-87163ff44845"></td>
  </tr>
  <tr>
    <td>Light And Darkness</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/LightAndDarkness.png" width="256" ></td>
    <td><img src="https://github.com/user-attachments/assets/d9c4f37e-59a2-43b1-96d5-e17a72c8d68b"></td>
  </tr>
  <tr>
    <td>Solus Factor Unleashed</td>
    <td><img src="https://github.com/1TeaL/Shigaraki/blob/main/ShiggyUnity/Assets/Shigaraki/GUI/CombinedSkills/SolusFactorUnleashed.png" width="256" ></td>
    <td><img src="https://github.com/user-attachments/assets/1d10403b-00e5-46c9-9c71-fa3d7f3af6a8"></td>
  </tr>
</tbody>
</table>
</details>

	




## Numbers
##### Armor = 10 + 0.5 per level
##### Damage = 1 + 1 per level
##### Regen = 1 + 0.2 per level 
##### Health = 141 + 41 per level
##### Movespeed = 7

These stats are prone to change.

## Credits
##### HenryMod for the template.
##### Mr.Bones for supplying base shigaraki skill icons.
##### Finally, Thank you Horikoshi Sensei for creating this legendary series.

rest of changelog on github.
## Changelog

<details>
<summary>Click to expand previous patch notes:</summary>
- 2.4.0- Fixes
	- Updated code to work with newest updates, apologies for the long wait, next update(s) will aim to update his model + streamline and improve the quirk system.
 	- Fixed AFO quirk stealing- can now be used while moving	
 	- Fixed Beetle Queen- Summon quirk, can now summon Chef and False son as well, however I haven't implemented their quirks
  	- Air cannon no longer has reduced duration because of attack speed
- 2.3.2- Bug fixes
	- Slight changes to make sure damage from passives are properly added.
- 2.3.1- Bug fixes
	- Hopefully fixed issues with damage such as infinite lightning strikes on Prime Meridian.
- 2.3.0- Seekers of the Storm Update!
	- Updated to seekers of the storm!
 	- I had attempted to make all for one be a skill slot but that requires a bit of changes so it may come in a later update. Otherwise working on a rework of the current system so that Shigaraki can access all stolen quirks he has.
  	- My aim is to bring Shigaraki to his Prime so he'll change his look as well. Stay Tuned!
  	- One more thing, no new skills have been added at this point!	  
- 2.2.3- Bug Fixes/Changes
	- Bug Fixes
  		- Noted that when Shigaraki gets on a jump pad he doesn't go up properly? unsure on issue. For now, added a lot of height gain for air walk if you hold no direction, just the jump button.
    	- Changes
		- Changed Gacha so that it gives 1 of each item tier, previously would give 4 whites, 2 greens and 1 red and was too powerful.
- 2.2.2- Bug Fixes/Changes
	- Bug Fixes
  		- Fixed omnibuff giving double time stacks and not properly decreasing.
  		- Improved buff checks for passsive skills (hopefully)
      	- Fixed non-moving/moving passives. There were errors on checking if your character was not moving and now it checks if you aren't holding any direction down.
    - Changes
		- Changed Air Walk to now be able to fly forever as well as flying in where you're looking at if you're holding forward, allowing improved ascend/descend. This should help improve movement. Now also costs 10% of plus chaos every second instead and has no limit to flight.
      - There's now a disclaimer that when using AFO you need to be standing still. This has always been how it mechanically worked but it caused some confusion as you could not use it while moving. 
- 2.2.1- Bug Fixes/Changes
	- Bug Fixes
  		- Fixed some grammar mistakes with shadow claw and death aura.
  		- Fixed Gacha not incrementing to 30 stacks properly.
   	- Changes
		- Changed Big Bang from every 5th hit dealing 20% of the enemy's max HP to 1000% of the hit's damage. 20% of the enemy's hp was pretty..busted. This keeps it still strong for slower attacks but for high attackspeed skills it still has power.
- 2.2.0- 
	- Overall this update aims to fix big bugs, changing a lot of skills, improve the descriptions of skills, and maybe the start of a better explanation for shigaraki. A Miro link has been attached (although not the best looking) to show a tree of Shigaraki's abilities and their combinations. Future guides and tutorials for the character to come along with more reworks and changes. 
	- Bug Fixes
  		- Fixed Left hand sword particle not disappearing when using mercenary's wind assault
    		- Fixed The World not properly pausing animations of enemies- now it does!
      		- Fixed Glacial Elite's freeze sphere infinitely exploding on players
      		- Fixed Barbed Spikes not being created when Gup-Geep-Gip and Brass contraption paired together (was making Orbital Spikes).
      		- Fixed all spherical range indicators for auras or specific skills not appearing on top of the player.
		- Fixed errors when giving quirks to allies. Quirks can also no longer be given to enemies. 
      		- Fixed Clay Templar's Minigun to now properly scale with player's damage (was doing 0.3 damage before..).
      		- Similarly Fixed Lunar Chimera's Minigun to now properly scale with player's damage (was doing 3 damage before..).
      		- Fixed Sweeping Beam scaling with attackspeed inappropriately, shortening the duration but therefore reducing the spray range. Now attackspeed increases the number of bullets shot and the angle and total duration remains the same.
        	- Fixed Jellyfish's Regenerate to properly decrease per second.
   	- Changes
		- ALL SKILLS- now have descriptions on what they pair with and what they upgrade to for better clarification to players.
		- Added Risk of Options functionality to the Give Quirk hotkey.
      		- Lemurian's Fireball no longer is effected by gravity- the range was too limiting before. 
      		- Orbital Strike now appropriately works when letting go of the button as well as uses the left hand (now a secondary skill similar to Lunar Chimera's Minigun) so that the player is able to use it while using most skills.
      		- Similarly, Elder Lemurian's Fire Blast is also a secondary skill, allowing the player to hold it to charge it while using most skills. It also charges at a faster rate (approx 2x?).
      		- Blast Burn's AOE now appropriately scales to match the size of the explosions, and also scales in damage for each pulse. 
      		- Omniboost has been fixed but also instead the player loses half their current stacks on kill. Along with certain skills it enabled ludicrous amounts of attackspeed and damage to stack while only reducing it's rate at 1 per second was too slow. This change makes it still a strong skill against high health targets, but against squishier mobs it won't go out of control. 
      		- *REWORKED*- Expunge now does an AOE around the player, it now also scales with the NUMBER of debuff stacks an enemy has as well as the number of decay stacks. For each stack, it does an additional 150% damage additively instead. Before, it's limited range and requirement for aiming made it's use limited. Attackspeed still does increase it's range. 
   	- Known Issues
		- Noted some known multiplayer issues like doubling of effects or whatnot, aiming to fix that over time. 
- 2.1.1- Bug Fixes/Changes
	- Bug Fixes
  		- Fixed Shopkeeper/Newt All For One acquisition.
    		- Fixed Thunderclap making Shigaraki stuck.
   	- Changes
		- Added new Config to allow voices to be played.
- 2.1.0- Bug Fixes/Changes
	- Bug Fixes
  		- Defensive Microbots (Captain) icon fixed to P instead of A.
		- Attempted to fix omniboost again?
		- Fixed One For All not being able to be gotten from Deku.
   	- Changes
		- Added new method to obtain One For All- Bazaar's Newt. This enables a method for players who don't have Deku installed and/or don't have teammates to play him. It also keeps one for all behind a lunar coin cost. In addition, One For All's skill description also states that Deku and Newt are how this skill can be obtained for players' understanding.
		- Added information to all ultimate skills on the synergy skill pair required for players' understanding. Not knowing which skills made what was a bit confusing. 	
- 2.0.5- Bug Fixes/Changes    
    - Bug Fixes
    	- Allowed Huntress's Flurry quirk and Blackhole glaive quirk to damage void targets as well.
    - Changes
    	- Omniboost has been changed to simply reduce the number of stacks by 1 every second. There has been some weird bugs with halving it's stacks every 6 seconds for some reason.
- 2.0.4- Bug Fixes/Changes    
    - Bug Fixes
    	- Allowed Huntress's Flurry quirk and Blackhole glaive quirk to damage neutral targets as well such as eggs.
    	- Further improved All For One logistics, there were issues where it would grab the base quirk instead of the upgraded one. 
    	- Fixed typo with Final Release minimum plus chaos requirement. 
- 2.0.3- Bug Fixes/Changes    
    - Bug Fixes
    	- Fixed Stone Golem Laser's quirk cooldown from not decreasing.
    	- Prevented Huntress's Flurry quirk and Blackhole glaive quirk from attacking allies.
    - Changes
    	- Noted bug that sometimes quirks don't combine.
    	- Improved effects for Decay debuff.
- 2.0.2- Bug Fixes/Changes    
    - Bug Fixes
    	- Fixed Decay debuff causing some errors, also fixed the decay awakened to only work on shigaraki.
    	- Fixed issues with taking fall damage that was affecting everyone.
    	- Fixed Tar boost (clay dunestrider) and Spirit boost (Greater wisp) buffs giving 8 buff stacks instead of 8 second durations.
    	- Grandparent sun was not reliably working- attempted to fix it.
    	- ?Fixed omniboost not removing stacks.
    - Changes
    	- Changed Grandparent sun to centre on Shigaraki instead of a target you're looking at. Also changed it to drain 10 plus chaos per second instead and lowered to cooldown from 30 seconds -> 5 seconds. Added new animation for it as well.
    	- Allowed air walk height gain to be infinite only if you're not holding any direction.
    	- Added Decay's impact effect on more skills like extreme speed.
- 2.0.1- Bug Fixes/Changes    
    - Bug Fixes
    	- Fixed quirks not upgrading properly.
    	- Fixed logic with getting ultimate quirks, before you could get it as long as you had 3 of the required base quirks not 4 as intended.	
    	- Fixed Decay Plus Ultra particle effect rotation. 
    	- Fixed some typos.
    - Changes
    	- Added Decay Plus Ultra slamming animation.	
    	- Added more tips such as using the beetle queen ability to summon survivors for their skills.
- 2.0.0- THE SYNERGY UPDATE!    
    - New mechanics 
    	- Quirks can now SYNERGISE, creating a BRAND NEW SKILL/PASSIVE!
    		- With 60 base quirks, that makes 30 synergised quirks, which can also combine to make 15 ultimate quirks! 
    		- ``I CAN NOW SAY SHIGARAKI HAS 100+ SKILLS/PASSIVES!!``
    		- By grabbing the specific pair of a skill while having the skill in your skill slots, you will automatically get the synergised skill. If you had the synergised skill's pair in your skill slots it will automatically get the ultimate skill as well.
        - Also to accommodate synergies, Shigaraki can only have 1 of each skill, no more duplicates. This means that after getting the ultimate or synergised version, you won't get that version if you try and steal someone's quirk and get the tier below.
        - ``A new Config option is added to allow all skills to be chosen in the loadout if you'd prefer to build Shigaraki from the beginning (I can't speak much on balance but why not). All for One functionality is still present if you'd like to use it. You'll need to restart the game to see changes.`` 
    	- Added a plus chaos meter
    		- Shigaraki starts with 100. 
    		- Leveling up increases the max by 10.
    		- Getting stock based items increase the max as well (back-up magazines: 10, afterburners: 30, lysates: 15)
    		- Getting cooldown reduction items decrease the cost (alien head: 25% reduction, purity: 5 flat reduction) 
    		- Stealing quirks now costs 50 plus chaos. 
    		- Plus chaos regenerates over time, killing enemies gains 10% of max. 
    	- Added ability for Shigaraki to trigger expose- reducing cooldowns by 1 and dealing 350% bonus damage like mercenary. 
    		- Killing enemies will grant plus chaos.
        - Added new ability- give quirk: Able to give passive quirks to allies- an ally is able to have one passive at a time. 
        	- In addition, addded passive explanations to their buffs so allies can read them.
    - Changes    	 
    	- Added proper description...(sorry Ditto)
    	- Cleaned up explanations of skills, removed redundant information like Agile. 
    	- Made it so the extra [information] under each skill is more relevant to what character's quirk you'll need to get the synergised quirk.
    	- Along with this, all base skill icons are now changed to have the main character on the top left and the paired character on the bottom right to let players know who to look out for (In the future they may simply just have a new icon itself).
    	- Right handed and Left handed skills concept is scrapped. It was an arbitrary way of listing which skills could be held with others. Certain skills may still be animated with one hand or the other but more animations will attempt to involve both. 
    	- In general, skills will be able to be used in conjuction in a way that makes sense like base survivors. (Eg. you can use a buffing skill alongside an attacking skill- although ideas like being able to hold both the clay templar and lunar wisp minigun skills together will still be there).
    - Reworked Skills
    	- Decay: Instead of a slam it's a swipe in front of you, less janky in general.
    	- Decay DoT: 
    		- It no longer reapplies to the user, it now properly recognizes you are the attacker of the enemy's hit so you'll see their health bars properly.
    		- Damage now scales off of your damage instead of the enemy's damage, dealing the higher of 100% damage or 0.5% of the enemy's max health per second per stack.
    		- The instakill threshold has changed from 50-> 100 stacks- this is due to this update providing more ways to stack decay.
    		- All overlap attacks will apply decay (Including projectiles that have them *cough* Wind Slash *cough*). 
    	- Beetle: Strength boost now gives flat 5 damage, no melee bonus.
    	- Lesser wisp: Ranged boost changed to Haste-> gives flat 0.5 attack speed, no ranged bonus.
    	- Lunar exploder: Lunar aura changed to Lunar barrier-> gives shield equal to 25% of max health.
    	- Jellyfish: Nova explosion changed to Regenerate-> Store half the damage you take, activate skill to heal.
    	- Beetle queen: Acid shotgun changed to Summon Ally-> Summon a survivor that inherits all your items (allows quirks to be taken from them too).
    	- Mercenary: Eviscerate changed to Wind Assault-> Dash forward through enemies, applying expose.
    	- Grovetender: Hook Shotgun changed to Chain-> Immobilize nearby enemies for 6 seconds.
    	- Multiplier: 
    		- changed to constant active that costs plus chaos meter per hit. 
    		- Multiplier no longer triples projectiles and decay ticks anymore due to this. 
    		- Multiplier also no longer triples Shigaraki's current damage passively, just the damage on-hit: Unintendedly would triple damage for DoTs without spending anything.
    	- Loader: Scrap barrier-> Gain 1% of max hp as barrier on all hits- not just melee atacks and no more damage boost for melee attacks.
    	- Vulture: Flight changed to Wind Blast-> Create a gust of wind, pushing enemies and stunning them for 200% damage.
    - Balance Changes
    	- No more melee and ranged arbitrary discrepancy between skills to streamline balance.
    	- Cooldown changes for a lot of skills, longer in general to balance out how many skills able to be used along with item changes with the extraskill slots.
    	- Damage and proc coefficient changes on a lot of skills.
    	- Some changes below but not all:
    		- Bullet laser correctly has a proc coefficient of 0.3 instead of 1 for each bullet. Now does 5x200% damage instead of 5x300%, as it was a lot stronger than other skills. 
    		- Both Tar Boost and Spirit Boost has been changed to give 8 second buffs but are no longer stackable.
    		- Bandit's skill Light's Out damage buffed from 300%-> 600%.
    - Bug fixes
    	- Double tap correctly has a proc coefficient of 1 instead of 0. 
    	- Remove skill now properly works for all skills.

- 1.3.1
    - updated risk of option config for holdbuttonAFO, now it's a slider that goes from 0-10 instead of a % slider like for volume. DELETE YOUR CONFIGS!
- 1.3.0
    - All for one Rework! Set up for the 2.0 synergy update.
        - Added dedicated button (default: F) to steal quirks! Press it then press another skill slot to put the skill into that slot!
        - Added dedicated button (default: V) to remove quirks! Press it then press another skill slot to clear that skill slot!
        - Any skill can go to any slot now! Have 8 passives if you need to AFK! Have 8 actives if you want to crank that APM!
        - Stock increasing items such as back-up magazine increase the stocks for the associated extra skill slots above the skills.
        - Passives no longer have AFO functionality. They simply provide the buff incase they didn't when pressed, nothing big.
    - Risk of Options mod support!
        - Changed config for instant AFO, now you can type how long you want to hold the button for (default: 0 seconds).
    - Changed artificer elementality quirk to work properly if you had multiple of it, so instead of using one skill and it changing all, it now changes individually. Also works with extra skill slots now as well.
    - Added 60 second timer for elite aspects to limit aspect copying.
    - Bug fixes:
        - Fixed some skills not giving buffs, fixed buffs being provided to non-hosts in general, all for one should be multiplayer friendly now.
        - buff skills like Clay dunestrider and Greater wisp should now properly add more seconds to their buffs if you're able to.
    	- Disabled overlays for the Hand so that there isn't a weird effect with personal shield generator or similar effects, it does mean it won't go invisible with the body.	
- 1.2.3 
    - Bug fixes:
    	- Fixed grandparent not having the right indicator
	    - Fixed solus mini passive, when going to a new stage/reviving being put onto a skill slot rather than an extra skill slot.
	    - Fixed greater wisp buff giving clay dunestrider buff and fixed issues with durations for both of them.
- 1.2.2 
    - Fixed missing networking API.
- 1.2.1 
    - Added Imp Boss to the passive indicator list (missed it).
    - Non-hosts can now get elite aspects as well.
- 1.2.0 
    - New Icons! (Courtersy of Mr.Bones) 
    - Model setup for skin by Mr.Bones
- 1.1.4 
    - Fixed quirk resetting to properly reset the quirk next stage.
    - Known issue, non-hosts can spawn the elite aspect but not pick them up.
- 1.1.3 
    - Fixed multiplier giving every buff(again).
    - Possibly fixed multiplayer non-hosts having AFO work with them.
- 1.1.2 
    - Fixed fps lag from future runs and current runs(Hopefully).
    - Buffs carry over on respawn/next stage instead of needing to reactivate the skill.
- 1.1.1 
    - Hopefully fixed decay buff spreading causing freezing/lag.
    - Added an impact effect for Decay primary as well as when the buff spreads.
- 1.1.0 
    - Added All Survivor Quirks! (except heretic)
    - Added Elder lemurian's skill (forgot about it oops)- Fire blast.
    - Fixed right handed animations not playing.
    - Fixed beetle buff not increasing melee attack damage.
    - Fixed readme missing stats info for actives.
    - Balance changes/Reworks: 
         - You can now hold AFO for 3 seconds and it will remove your current passive and quirk that matches the button pressed for.
         - Improved Aircannon speed and movement.
         - Buffed decay to now add one stack and also spread every 6 seconds. The damage is also done in 1 second intervals instead, the damage now deals either the greater of 50% of the ENEMY'S damage or 0.5% of their maximum HP per stack.
         - Impboss bleed buff now affects every attack, Acrid's poison buff works like this as well.
         - Reworked a few quirks as they were not that exciting, and served not too much purpose currently, simply different projectiles which didn't have many unique effects.
             - Greater wisp now grants a buff that makes attacks explode for 50% of the damage. 
             - Clay dunestrider now grants a buff that gives 10% lifesteal and 1.5x attackspeed. 
             - Solus probe quirk changed as glide felt a bit unintuitive to use, now it grants attackspeed as you hold down any button every second.
- 1.0.9 
    - Fixed bug where jumping disabled air control until collision.
    - Balance changes: 
         - Nerfed stone golem damage 600% -> 400%. 
         - Nerfed Strength and Ranged specific boosts 2x -> 1.5x.
- 1.0.8 
    - Added separate handless skin.
    - Fixed bug on not carrying over passives through levels.
- 1.0.7 
    - Oops multiplier applied all buffs.
- 1.0.6 
    - Forgot to mention fix for blind vermin passive breaking.
- 1.0.5 
    - CustomEmotesAPI compatibility added
    - Added an icon for the default skin
    - Optimised AFO code
    - Buff acquisition optimised and a lot faster now
    - Fixed gup being set as an active skill rather than extra skill
- 1.0.4 
    - Fixed errors when entering a new stage
    - Added back different buff icons
    - Multiplier now doesn't get taken away on DoT damage such as decay
         - Melee attacks are now easier to trigger the triple decay effect due to this 
    - Bullet Laser now works
    - Improved AFO passive logic, should be quicker to receive the buff now
    - Mentioned that AFO can grab elite equipment
    - Smoother animations
    - Added new config so you have to hold the button for 1 second before stealing a quirk.
- 1.0.3 
    - buff icons not loading so for now everything is warcry buff icon 
- 1.0.1 
    - hotfix dependency 
- 1.0.0
    - Release.
- rest of changelog on github

</details>

 
<details>
<summary>Click to expand for OG pictures:</summary>

## OG Pictures
![128x128Icon](https://user-images.githubusercontent.com/93917577/168004591-39480a52-c7fe-4962-997f-cd9460bb4d4a.png)
![Character Select](https://github.com/user-attachments/assets/1dbefd26-8eaf-4c47-9a2e-3ff739613af9)
![QuirkUI](https://github.com/user-attachments/assets/bf843ff3-8f62-4fb5-8c77-00b35c9cc271)

![DecaySwingPic](https://github.com/user-attachments/assets/48485919-aeec-4904-bf72-44a62d1fe48d)
![DecayPlusUltraPic](https://github.com/user-attachments/assets/0c627cec-a39e-4a77-a9e8-11600461f661)
![AFOStealPic](https://github.com/user-attachments/assets/a28807f5-6b61-4d33-be9c-96bb20d6a5bb)

![Passive](https://github.com/1TeaL/Shigaraki/assets/93917577/07984117-9535-4e63-af1b-7b5e1367a3b3)

![AllForOne](https://github.com/1TeaL/Shigaraki/assets/93917577/94c409bd-fe00-4e02-bbcb-ac8c4bdf5e13)

![Decay](https://github.com/1TeaL/Shigaraki/assets/93917577/6e05d78c-c471-4c59-ad86-c248db977fa4)
![BulletLaser](https://github.com/1TeaL/Shigaraki/assets/93917577/cff3ea37-9def-469b-9de0-32d3b84e4d11)
![AirCannon](https://github.com/1TeaL/Shigaraki/assets/93917577/28fc6fea-c70c-4d9f-b28b-bcf1c767ccbd)
![Multiplier](https://github.com/1TeaL/Shigaraki/assets/93917577/672195fb-94ca-45af-a3e4-850234f3e560)

![Barrier](https://github.com/1TeaL/Shigaraki/assets/93917577/e368b22b-7e56-442f-92fa-1b67b88fd2d3)
![StrengthBoost](https://github.com/1TeaL/Shigaraki/assets/93917577/49158768-506d-4342-b881-e5861f87e76a)
![JumpBoost](https://github.com/1TeaL/Shigaraki/assets/93917577/17c595f6-7ee2-4b9a-b8ef-468356161cc1)
![SuperSpeed](https://github.com/1TeaL/Shigaraki/assets/93917577/090d64e4-9cd2-41f0-b2e4-bcaf831913ed)
![SpikyBody](https://github.com/1TeaL/Shigaraki/assets/93917577/e77b247f-9f32-4838-81d3-346c33026126)
![Mortar](https://github.com/1TeaL/Shigaraki/assets/93917577/299399c2-b462-4b08-8000-0c40b0370153)
![AcidJump](https://github.com/1TeaL/Shigaraki/assets/93917577/ac4054ec-035b-4c26-ad00-c0bbfa0e1fba)
![Haste](https://github.com/1TeaL/Shigaraki/assets/93917577/a6f69af8-378b-4a6e-ac3d-9013e57c6268)
![LunarBarrier](https://github.com/1TeaL/Shigaraki/assets/93917577/d02e76d6-9b4e-4489-a831-1127dafcf00f)
![HealingAura](https://github.com/user-attachments/assets/f8aa2d8e-2a20-4d82-85a5-72018a524661)
![SolusBoost](https://github.com/1TeaL/Shigaraki/assets/93917577/a452f7b4-7f8e-44fa-ade5-93c3b2903baf)
![VoidMortar](https://github.com/1TeaL/Shigaraki/assets/93917577/34ae7614-a775-4ca2-9dbc-6f8824f99558)
![Gravity](https://github.com/1TeaL/Shigaraki/assets/93917577/e92eb812-a12a-43dd-b762-b673e7095bb6)
![Bleed](https://github.com/1TeaL/Shigaraki/assets/93917577/f4a90506-e103-4d54-b31a-09c296be1a22)
![StoneSkin](https://github.com/1TeaL/Shigaraki/assets/93917577/f1dd6ae4-08a9-44bb-aecd-325d6fc2ff6e)
![SolusPriming](https://github.com/user-attachments/assets/57340141-b3ee-474e-b744-5244ddaa9aa5)
![CritBoost](https://github.com/user-attachments/assets/c0797cf6-4e46-42d5-a81c-a83df3ea873f)
![EquipmentBoost](https://github.com/user-attachments/assets/5895fef2-40c4-45e8-a723-63c203dd6bec)
![BlazingAura](https://github.com/1TeaL/Shigaraki/assets/93917577/650ca0ec-0bd1-4a17-8c72-d57cec64352c)
![LightningAura](https://github.com/1TeaL/Shigaraki/assets/93917577/3c71f9ab-a87e-4244-97f2-b13e76e91c36)
![Vagrant'sOrb](https://github.com/1TeaL/Shigaraki/assets/93917577/40bbe57f-736d-4614-a117-b97bd790d392)
![Poison](https://github.com/1TeaL/Shigaraki/assets/93917577/f42e4034-191a-426c-b884-2fa7f4f0b37e)
![DoubleTap](https://github.com/1TeaL/Shigaraki/assets/93917577/9542d230-035b-4a3b-942a-ddbb79c648d9)
![DefensiveMicrobots](https://github.com/1TeaL/Shigaraki/assets/93917577/d2e22b41-f977-4574-8d0b-3707cc4b7ea8)
![ScrapBarrier](https://github.com/1TeaL/Shigaraki/assets/93917577/de311eca-97b2-4fd9-8779-7b35b028a567)
![OilBurst](https://github.com/user-attachments/assets/4451ea4e-9c20-49b3-97c3-97ee36614a41)
![StolenInheritance](https://github.com/user-attachments/assets/8e77c42b-c78d-43b0-8dbe-707ba34039a5)

![BigBang](https://github.com/1TeaL/Shigaraki/assets/93917577/90f108ea-4c51-4095-a95c-fb7a10f28f45)
![Wisper](https://github.com/1TeaL/Shigaraki/assets/93917577/0a35bbdc-3250-4aee-aa91-e34a49065dda)
![Omniboost](https://github.com/user-attachments/assets/75de7000-5a50-4898-974b-5295ed469caa)
![Gacha](https://github.com/1TeaL/Shigaraki/assets/93917577/0d6d323e-747a-4b0e-8412-d16ea9e0da94)
![StoneForm](https://github.com/1TeaL/Shigaraki/assets/93917577/8e3186fc-58d1-40b7-913a-04af350a1a19)
![AuraOfBlight](https://github.com/1TeaL/Shigaraki/assets/93917577/d8f4f096-258e-4ae7-9e23-392c699c8358)
![BarbedSpikes](https://github.com/1TeaL/Shigaraki/assets/93917577/0046f909-42f2-41f8-abc5-f396ee14e78a)
![Ingrain](https://github.com/1TeaL/Shigaraki/assets/93917577/910877c4-fd5f-4390-bc61-ada92cb4ed59)
![DoubleTime](https://github.com/1TeaL/Shigaraki/assets/93917577/90df831f-eb72-4004-8d0d-72953d2df24f)
![BlindSense](https://github.com/1TeaL/Shigaraki/assets/93917577/5fb63eea-00d9-4599-ac94-4b121e662b7d)
![HyperRegeneration](https://github.com/user-attachments/assets/5103971f-fa17-4825-9754-dadab80d0fff)

![Supernova](https://github.com/1TeaL/Shigaraki/assets/93917577/4fe69668-9879-4c44-9767-6e9078e4ce2e)
![Reversal](https://github.com/1TeaL/Shigaraki/assets/93917577/c7102544-c459-4ed3-9c1b-142f1f50edc5)
![MachineForm](https://github.com/1TeaL/Shigaraki/assets/93917577/1ee8e45e-8f4b-4466-aec5-b5551efe731d)
![GargoyleProtection](https://github.com/1TeaL/Shigaraki/assets/93917577/cbef67d4-6dcc-4df2-9f9b-839c10f8990f)
![WeatherReport](https://github.com/1TeaL/Shigaraki/assets/93917577/fbc401a3-ebff-4e2c-93dd-9f85b94d914f)
![DecayAwakened](https://github.com/1TeaL/Shigaraki/assets/93917577/88c94925-7b16-4966-a0d8-0c4b8285e8b0)

![WindBlast](https://github.com/1TeaL/Shigaraki/assets/93917577/1bcc8647-b2b1-4098-a088-85e3323fe870)
![FastDrop](https://github.com/1TeaL/Shigaraki/assets/93917577/006823bd-9f3e-4325-b2dc-7fd783323626)
![Charging](https://github.com/1TeaL/Shigaraki/assets/93917577/a1a61e45-ab3e-4753-8efe-1013ee1085f2)
![SpikedBallControl](https://github.com/1TeaL/Shigaraki/assets/93917577/33809b62-4ede-4967-a16f-a949be6790b1)
![ClayAirStrike](https://github.com/user-attachments/assets/d01ed3f6-de5e-4a3e-a872-91271283212b)
![ClayMinigun](https://github.com/1TeaL/Shigaraki/assets/93917577/66a3f815-838f-4d71-8a6b-861bfd3f43b7)
![FireBlast](https://github.com/1TeaL/Shigaraki/assets/93917577/3e7c2b0b-8b60-46bb-9a88-63f72536166e)
![SpiritBoost](https://github.com/1TeaL/Shigaraki/assets/93917577/1d326440-c26d-4081-a8f9-050f36cdc1fc)
![Blink](https://github.com/1TeaL/Shigaraki/assets/93917577/af278b95-3a84-4e49-bb73-1e62b0cfb7d0)
![Regenerate](https://github.com/user-attachments/assets/63cdee5b-a4f5-46cd-836c-55ac0004c740)
![Fireball](https://github.com/1TeaL/Shigaraki/assets/93917577/d52879af-40be-405c-881b-aab3c3e711b7)
![SlideReset](https://github.com/1TeaL/Shigaraki/assets/93917577/f8f2d6fe-9075-4e3d-b536-f4900d2c341a)
![LunarMinigun](https://github.com/1TeaL/Shigaraki/assets/93917577/9d262f32-66fd-4ad3-a178-349423f25544)
![SolusPlantMine](https://github.com/user-attachments/assets/7b59ce02-1f8d-4901-ad45-247d14ec6108)
![SolusExtract](https://github.com/user-attachments/assets/cb341dcf-cfb3-48f5-8373-5407dd0d1bd7)
![SolusInvalidate](https://github.com/user-attachments/assets/8fb08d47-017e-420b-a0e5-8e70249f09bd)
![SolusAccelerate](https://github.com/user-attachments/assets/9cb23bef-38d3-4b9a-bd57-c482def25a10)
![SolusTransport](https://github.com/user-attachments/assets/8dcddd61-d5f0-4eb5-883a-9c3cd90c794e)
![Teleport](https://github.com/1TeaL/Shigaraki/assets/93917577/edb227f5-ae8c-4bdd-9220-ba0bb714af10)
![Laser](https://github.com/1TeaL/Shigaraki/assets/93917577/bd652eb3-ce3e-4c26-a7c7-d785d59b8cf5)
![NullifierArtillery](https://github.com/1TeaL/Shigaraki/assets/93917577/1744df7f-5e82-4bea-aec2-81567f8a78cb)
![SummonAlly](https://github.com/1TeaL/Shigaraki/assets/93917577/e8b4ff1b-d823-43a4-bb83-a9761a9aaede)
![SolarFlare](https://github.com/1TeaL/Shigaraki/assets/93917577/577dbae0-2895-4e87-839d-21239e2d84ea)
![Chain](https://github.com/1TeaL/Shigaraki/assets/93917577/6819cf3d-48e0-45b5-a3d9-621cbfacad30)
![TarBoost](https://github.com/1TeaL/Shigaraki/assets/93917577/06464c70-b7fe-4365-9745-8cb740b3c7fd)
![AntiGravity](https://github.com/1TeaL/Shigaraki/assets/93917577/e59a836e-091a-4eac-87e0-017910ce3c4b)
![Beam](https://github.com/1TeaL/Shigaraki/assets/93917577/62f8bc78-97c5-403b-8bed-4d937a1857b5)
![VoidMissiles](https://github.com/1TeaL/Shigaraki/assets/93917577/47e96504-b2e3-47e8-a853-377e27a0fe54)
![ThrowThqwibs](https://github.com/1TeaL/Shigaraki/assets/93917577/7598bd7e-8794-441d-9ecb-9ff4526f29d5)
![Elementality:Fire](https://github.com/1TeaL/Shigaraki/assets/93917577/95f99bf3-77a5-46a4-ac03-64a564e84642)
![Elementality:Ice](https://github.com/1TeaL/Shigaraki/assets/93917577/1c83e245-d02e-4c2f-af11-e495a7a0a5e8)
![Elementality:Lightning](https://github.com/1TeaL/Shigaraki/assets/93917577/f9289f1b-7782-4d17-ac9c-3776f57ab8cb)
![LightsOut](https://github.com/1TeaL/Shigaraki/assets/93917577/f7514ed6-265d-454e-9f82-d01a65a98723)
![Turret](https://github.com/1TeaL/Shigaraki/assets/93917577/4add3d58-dfcd-416d-bd31-0723974323a4)
![Flurry](https://github.com/1TeaL/Shigaraki/assets/93917577/b412f6c7-3b64-497c-92e5-0a89fec7dec5)
![WindAssault](https://github.com/1TeaL/Shigaraki/assets/93917577/bb00a919-2fb8-4f00-a66a-9e2b7b626345)
![PowerStance](https://github.com/1TeaL/Shigaraki/assets/93917577/bb77e8c1-defb-4fbe-8e79-05fb6fe51811)
![CryochargedRailgun](https://github.com/1TeaL/Shigaraki/assets/93917577/1c242da8-f6cf-40b2-8050-d843da9896bc)
![SeedBarrage](https://github.com/1TeaL/Shigaraki/assets/93917577/29c939e9-0c2f-44e0-8bf0-90b97855e728)
![Cleanse](https://github.com/1TeaL/Shigaraki/assets/93917577/7fe0ec45-7366-42f5-a1e8-faf36a0b8eb6)
![OneForAll](https://github.com/1TeaL/Shigaraki/assets/93917577/788ecfb5-eb24-4b06-845f-3bc974e70fb7)

![SweepingBeam](https://github.com/1TeaL/Shigaraki/assets/93917577/424a8f36-9f47-4ba6-9691-45e573e96dc3)
![BlackholeGlaive](https://github.com/1TeaL/Shigaraki/assets/93917577/473fa1e0-db6c-41fb-9c26-91ac3d8912ac)
![GravitationalDownforce](https://github.com/1TeaL/Shigaraki/assets/93917577/e4666ccf-fd54-4107-9a76-243a5236ca61)
![WindShield](https://github.com/1TeaL/Shigaraki/assets/93917577/eb33d7ed-ed36-44e5-acd2-e454c562906b)
![Genesis](https://github.com/1TeaL/Shigaraki/assets/93917577/4be012f2-e65f-4504-b2ef-67ccddabdd55)
![Refresh](https://github.com/1TeaL/Shigaraki/assets/93917577/d565a767-7444-4a44-beae-e39c42054f71)
![Expunge](https://github.com/1TeaL/Shigaraki/assets/93917577/fbf13ead-425b-4c0d-a34c-ded12e95d940)
![ShadowClaw](https://github.com/1TeaL/Shigaraki/assets/93917577/4aa46278-cd6a-49a6-8aed-99673d6b4c03)
![OrbitalStrike](https://github.com/1TeaL/Shigaraki/assets/93917577/391e5457-9af1-424f-85c5-dbd3ca1fb074)
![Thunderclap](https://github.com/1TeaL/Shigaraki/assets/93917577/efd380ad-114f-4b6a-aef3-225262bcf918)
![BlastBurn](https://github.com/1TeaL/Shigaraki/assets/93917577/ad51135f-3ce7-408e-a5c7-8aceb972aa46)
![BarrierJelly](https://github.com/1TeaL/Shigaraki/assets/93917577/3ac9b83b-8bf2-4aab-877e-8c4aa4ef0a3b)
![MechStance](https://github.com/1TeaL/Shigaraki/assets/93917577/ab750ef6-8a05-45f8-87c0-887e1e71e892)
![WindSlash](https://github.com/1TeaL/Shigaraki/assets/93917577/bd133143-1f70-4df4-808f-3a940ad32a27)
![LimitBreak](https://github.com/1TeaL/Shigaraki/assets/93917577/117072cd-7383-486e-b10c-f73bdbcf6459)
![VoidForm](https://github.com/1TeaL/Shigaraki/assets/93917577/6b1bd344-fe06-495f-aaba-9c6d4ea3ce6c)
![ElementalFusion](https://github.com/1TeaL/Shigaraki/assets/93917577/d2719c5e-d245-4b01-a97a-d168d5b41dfa)
![DecayPlusUltra](https://github.com/1TeaL/Shigaraki/assets/93917577/31b75bb8-69ea-4b06-8560-e421593e04a4)
![MachPunch](https://github.com/1TeaL/Shigaraki/assets/93917577/65ca0675-f7b9-419b-befe-d5c45e474685)
![Rapid Pierce](https://github.com/user-attachments/assets/89565371-fa6f-435d-addf-49738cdcf835)

![TheWorld](https://github.com/user-attachments/assets/0e13058d-acfc-4b75-98ab-65e468e98b8d)
![ExtremeSpeed](https://github.com/user-attachments/assets/9a013622-b67b-4bca-ae20-5307076c9fe0)
![DeathAura](https://github.com/1TeaL/Shigaraki/assets/93917577/cdc8ed14-bfa6-4ee0-a044-a51ee29eeef7)
![OneForAllForOne](https://github.com/user-attachments/assets/978c18fd-e427-4581-bcbc-bed90f765bba)
![XBeamer](https://github.com/user-attachments/assets/ef12d3ed-c860-4ee0-bd19-11cf6ed75b84)
![FinalRelease](https://github.com/user-attachments/assets/7039e330-f7de-4db9-9971-b418e605d3e9)
![BlastingZone](https://github.com/user-attachments/assets/3660855e-6285-4c26-bf21-61643610ab13)
![WildCard](https://github.com/user-attachments/assets/171f790b-9972-420a-a2b8-87163ff44845)
![LightAndDarkness](https://github.com/user-attachments/assets/d9c4f37e-59a2-43b1-96d5-e17a72c8d68b)
![SolusFactorUnleashed](https://github.com/user-attachments/assets/1d10403b-00e5-46c9-9c71-fa3d7f3af6a8)

</details>





























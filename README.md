# ShigarakiModRor2
Shigaraki Tomura mod for Ror2
Go Beyond, Plus Chaos!
## Shigaraki
Adds Shigaraki from My Hero Academia, an initially weaker survivor which can steal 'quirks' from enemies, gaining their abilites/skills.
#### Multiplayer works (hopefully). CustomEmotesAPI compatibility. Risk of Options support.
#### Message me on the Risk of Rain 2 Modding Discord if there are any issues- TeaL#5571.
#### <a href="https://ko-fi.com/tealpopcorn"><img src="https://user-images.githubusercontent.com/93917577/160220529-efed5020-90ac-467e-98f2-27b5c162d744.png"> </a>
If you enjoy my work, support me on Ko-fi!
## Popcorn Factory
<details>
<summary>Check out other mods from the Popcorn Factory team!</summary>	
<div>
    <a href="https://thunderstore.io/package/PopcornFactory/Rimuru_Tempest_Mod/">
      <img width="130" src="https://cdn.discordapp.com/attachments/399901440023330816/1033003173759164467/unknown.png"/>
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
        <img src="https://cdn.discordapp.com/attachments/399901440023330816/960043614036168784/TeaL-DekuMod-3.1.1.png.128x128_q95.png"><br>
        <p>DekuMod (My other Mod!)</p>
    </a>
</div>
<div>
    <a href="https://thunderstore.io/package/Ethanol10/Ganondorf_Mod/">
        <img src="https://cdn.discordapp.com/attachments/399901440023330816/960043613428011079/Ethanol10-Ganondorf_Mod-2.1.5.png.128x128_q95.png"><br>
        <p>Ganondorf Mod (Ethanol 10)</p>
    </a>
</div>
<div>
    <a href="https://thunderstore.io/package/BokChoyWithSoy/Phoenix_Wright_Mod/">
        <img src="https://cdn.discordapp.com/attachments/399901440023330816/960054458790850570/BokChoyWithSoy-Phoenix_Wright_Mod-1.6.2.png.128x128_q95.png"><br>
        <p>Phoenix Wright Mod (BokChoyWithSoy)</p>
    </a>
</div>
<div>
    <a href="https://thunderstore.io/package/PopcornFactory/Wisp_WarframeSurvivorMod/">
        <img src="https://cdn.discordapp.com/attachments/399901440023330816/960043613692239942/PopcornFactory-Wisp_WarframeSurvivorMod-1.0.2.png.128x128_q95.png"><br>
        <p>Wisp Mod (Popcorn Factory Team)</p>
    </a>
</div>
</details>

## Latest Changelog, Next update(s)

- 2.0.0- THE SYNERGY UPDATE!    
    - New mechanics 
    	- Quirks can now SYNERGISE, creating a BRAND NEW SKILL/PASSIVE!
    		- With 60 base quirks, that makes 30 synergised quirks, which can also combine to make 15 ultimate quirks! 
    		- ``I CAN NOW SAY SHIGARAKI HAS 100+ SKILLS/PASSIVES!``
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
    		- All overlap attacks will apply decay (Including projectiles that have cause them *cough* Wind Slash *cough*). 
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

		


- Next update(s)
    - Bug fixes. 
    - More/Improved animations?
    - Icons for base skills.

<img src= "https://user-images.githubusercontent.com/93917577/179202355-c5eb5011-4295-4093-9c96-d84132bcbb05.PNG">

## Known Issues
    

## Overview
    He is initially weak with low base damage, aim to get passive quirks to increase his power.
    [LeftHanded] and [RightHanded] skills can be used simultaneously. 
    All For One can grab elite equipment from elites. 

## Skills
### Passive
Shigaraki can grab the target's quirk with All For One by pressing the F key by default, then pressing a skill slot. Remove quirks by pressing the V key by default and pressing the skill slot as well.

Actives(Circle Indicator) replace main skills and Passives(Square indicator) replace extra skills.

Both can be replaced at any time. 

All [Melee] attacks Decay, which decreases the enemies' movespeed and attackspeed as well as instakilling them at 50 stacks (except for Teleporter Bosses).

Shigaraki can sprint in any direction and has a double jump.

### Base Skills
<table>
<thead>
  <tr>
    <th>Skill</th>
    <th>                         </th>
    <th>Description</th>
    <th>Stats</th>
  </tr>
</thead>
<tbody>
  <tr>
    <td>Decay<br>Primary</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/179195239-9d1f2ecd-360f-4e35-86e5-20d173b44f16.png" width="100" height="100"></td>
    <td>Slam and Decay the ground/air around you, dealing 300% damage.<br><br><br><br><br>[Melee] [Decay]</td>
    <td>Proc: 1.</td>
  </tr>
  <tr>
    <td>Bullet Laser<br>Secondary</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/179195235-c3128f52-9292-4560-b194-8a6f24e1da5b.png" width="100" height="100"></td>
    <td>Shoot piercing lasers for 5x200% damage.<br><br><br><br><br>[Ranged]</td>
    <td>Proc: 0.3.<br>CD: 3s.</td>
  </tr>
  <tr>
    <td>Air Cannon<br>Utility</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/179195228-7a5b2f0e-4e45-4e50-96e3-e4281c319a97.PNG" width="100" height="100"></td>
    <td>Blasts an air shockwave behind you.<br>Does 400% damage and propels you forward.<br><br><br><br>[Melee] [Decay]</td>
    <td>Proc: 1.<br>CD: 5s.</td>
  </tr>
  <tr>
    <td>Multiplier<br>Special<br></td>
    <td><img src="https://user-images.githubusercontent.com/93917577/179195241-4e40f8ea-9df2-4d28-b372-409ced57dd85.png" width="100" height="100"></td>
    <td>Boosts your next attack to deal 3x damage.<br>Triples the number of projectiles, shots and decay stacks as well.<br><br><br><br>[Melee] [Ranged]</td>
    <td>CD: 8s.<br></td>
  </tr>
  <tr>
    <td>All For One</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/179195232-afa5af2b-de69-40bb-996f-f12c430dfe08.png" width="100" height="100"></td>
    <td>Steal the Target's quirk by pressing the F key by default, then pressing a skill slot.<br>Remove quirks by pressing the V key by default and pressing the skill slot as well.<br>Steal the elite aspect as well.</td>
    <td>CD: 1s.<br></td>
  </tr>
</tbody>
</table>


### Active Skills
<table>
<thead>
  <tr>
    <th>Active</th>
    <th>Quirk User</th>
    <th>Description</th>
    <th>Stats</th>
  </tr>
</thead>
<tbody>
  <tr>
    <td>Flight</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007147-086e88f1-7db8-4999-8aa5-bee1282bf141.png" width="100" height="100"></td>
    <td>Jump and float in the air, disabling gravity for 10 seconds.</td>
    <td>CD: 6s.</td>
  </tr>
  <tr>
    <td>Fast<br>Drop</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007155-25889635-31b3-4b4e-ab37-5c3a2cfe7bfb.png" width="100" height="100"></td>
    <td>Drop and slam down, stunning and dealing 400% damage,<br>gaining 5% of your max health as barrier.<br>Damage, radius and barrier gain scales with drop time and movespeed.<br><br><br>[Melee] [Decay] [Movespeed]</td>
    <td>Proc: 1.<br>CD: 4s.</td>
  </tr>
  <tr>
    <td>Charging</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007161-0ee72842-8b6c-4d1c-9557-ea234823e512.png" width="100" height="100"></td>
    <td>Charge forward at super speed, and if you slam into a solid object,<br>generates a shockwave that stuns enemies for 600% damage.<br>Hold the button down to keep charging.<br>Damage and radius scales with charge duration.<br><br>[Melee] [LeftHanded] [Decay] [Movespeed]</td>
    <td>Proc: 1.<br>CD: 4s.</td>
  </tr>
  <tr>
    <td>Spiked<br>Ball<br>Control</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007170-e2ebc59c-17f5-4de1-8d02-30e318c3d6c5.png" width="100" height="100"></td>
    <td>Summon 3 spiked balls, then release them, dealing 400% damage per ball.<br><br><br><br><br>[Ranged] [LeftHanded]</td>
    <td>Proc: 1.<br>CD: 6s.</td>
  </tr>
  <tr>
    <td>Clay<br>Airstrike</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007172-6552e61f-ceda-4f04-bcac-815dead09b12.png" width="100" height="100"></td>
    <td>Release a tar shockwave, and send a mortar into the sky, <br>which rains down on enemies around you, both dealing 200% damage.<br><br><br><br>[Melee] [Ranged] [LeftHanded] [Decay]</td>
    <td>Proc: 1.<br>CD: 7s.</td>
  </tr>
  <tr>
    <td>Clay<br>Minigun</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007176-fd02e021-3715-4b70-bd53-6afffaca45f1.png" width="100" height="100"></td>
    <td>Shoot a rapid hail of tar bullets, tarring and dealing 30% damage per bullet.<br><br><br><br><br>[Ranged] [RightHanded] </td>
    <td>Proc: 0.05.<br>CD: 1s.</td>
  </tr>
  <tr>
    <td>Fire<br>Blast</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007178-25d002de-e8c7-4c24-851e-d3d66e1d8de2.png" alt="Image" width="100" height="100"></td>
    <td>Hold the button down to charge a fire blast,<br>When released, deals 200% damage per hit.<br>Number of hits and radius scales with charge duration.<br><br><br>[Melee] [RightHanded] </td>
    <td>Proc: 0.5.<br>CD: 1s.</td>
  </tr>
  <tr>
    <td>Spirit<br>Boost</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007185-5550654a-c3e5-492c-bf07-d6dd3fb96a66.png" alt="Image" width="100" height="100"></td>
    <td>For 4 seconds, your attacks explode, dealing 50% of the attack's damage.<br>Additional uses adds to the current duration.<br><br><br><br>[RightHanded]</td>
    <td>CD: 8s.<br></td>
  </tr>
  <tr>
    <td>Blink</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007200-8c9dc669-4a8f-4bb7-aa1e-0ead343532ac.png" alt="Image" width="100" height="100"></td>
    <td>Blink a short distance away, scaling with movespeed.<br><br><br><br><br>[LeftHanded]</td>
    <td>Stocks: 3.<br>CD: 4s.</td>
  </tr>
  <tr>
    <td>Nova<br>Explosion</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007207-38b54a28-a06b-4f8f-b7b7-6696d5b1f132.png" alt="Image" width="100" height="100"></td>
    <td>Detonate an explosion on the target, stunning and dealing 2000% damage.<br>This explosion can hurt the user as well.<br>Radius scales with attackspeed.<br><br><br>[Ranged] [RightHanded]</td>
    <td>Proc: 2.<br>CD: 6s.</td>
  </tr>
  <tr>
    <td>Fireball</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007211-38ef12c8-4100-4b3e-9a95-796cc7caf119.png" alt="Image" width="100" height="100"></td>
    <td>Shoot a fireball, burning and dealing 200% damage.<br><br><br><br><br>[Ranged] [LeftHanded]</td>
    <td>Proc: 1.<br>CD: 0.</td>
  </tr>
  <tr>
    <td>Slide<br>Reset</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007218-c1c599b1-1e95-4c0d-941a-e0c0c125a3ed.png" alt="Image" width="100" height="100"></td>
    <td>Slide, resetting cooldowns for all other skills.<br><br><br><br><br>[LeftHanded]</td>
    <td>Proc: 1.<br>CD: 8s.</td>
  </tr>
  <tr>
    <td>Lunar<br>Minigun</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007219-b22e2574-2199-42b9-b214-524a3d41a865.png" alt="Image" width="100" height="100"></td>
    <td>Shoot a rapid hail of lunar bullets, crippling and dealing 300% damage per bullet.<br><br><br><br><br>[Ranged] [LeftHanded] </td>
    <td>Proc: 0.2.<br>CD: 1s.</td>
  </tr>
  <tr>
    <td>Teleport</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007230-0a790525-76f8-4f92-8a8c-e1988d0432f7.png" alt="Image" width="100" height="100"></td>
    <td>Teleport to the target and generate a shockwave on arrival that stuns enemies,<br>dealing 600% damage in a radius.<br><br><br><br><br>[Melee] [Decay]</td>
    <td>Stocks: 2.<br>Proc: 1.<br>CD: 5s.</td>
  </tr>
  <tr>
    <td>Laser</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007243-0ba456af-680c-499c-b633-2d427137cc55.png" alt="Image" width="100" height="100"></td>
    <td>Hold the button to charge a laser which, when released, deals 400% damage.<br>Damage and radius scales with charge duration.<br><br><br><br>[Ranged] [LeftHanded]</td>
    <td>Proc: 1.<br>CD: 5s.</td>
  </tr>
  <tr>
    <td>Nullifier<br>Artillery</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007263-d9c400f5-9b56-4c26-bdde-5776b2ca92a4.png" alt="Image" width="100" height="100"></td>
    <td>Hold the button down to constantly summon nullifier bombs on the target,<br>dealing 200% damage per bomb.<br><br><br><br>[Ranged] [RightHanded]</td>
    <td>Proc: 1.<br>CD: 1s.</td>
  </tr>
  <tr>
    <td>Acid<br>Shotgun</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007156-fa51d78c-823c-438d-94ff-18b83f681ebc.png" alt="Image" width="100" height="100"></td>
    <td>Shoot an acid shotgun infront of you for 5x400% damage, <br>leaving an acid puddle on the ground.<br><br><br><br>[Melee] [RightHanded]</td>
    <td>Proc: 1.<br>CD: 4s.</td>
  </tr>
  <tr>
    <td>Solar<br>Flare</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007184-f5cc4f46-1f08-40ad-9ee2-1c3cef4fe3e9.png" alt="Image" width="100" height="100"></td>
    <td>Hold the button to summon a miniature sun. <br>Sprinting or letting go of the button cancels the skill.<br><br><br><br> [RightHanded]</td>
    <td>CD: 12s.<br></td>
  </tr>
  <tr>
    <td>Hook <br>Shotgun</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007189-4081a4b0-7160-4586-bb7e-8256a5bc51d2.png" alt="Image" width="100" height="100"></td>
    <td>Shoot 5 hooks sequentially, pulling enemies and dealing 300% per hook.<br><br><br><br><br>[Ranged] [LeftHanded]</td>
    <td>Proc: 1.<br>CD: 4s.</td>
  </tr>
  <tr>
    <td>Tar<br>Boost</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007174-7eac7d7d-d6d0-44ab-894f-d3f9c33b36d0.png" alt="Image" width="100" height="100"></td>
    <td>For 4 seconds, your attacks tar, gain 10% lifesteal and 50% attackspeed.<br>Additional uses adds to the current duration.<br><br><br><br>[RightHanded]</td>
    <td>CD: 8s.<br></td>
  </tr>
  <tr>
    <td>Anti<br>Gravity</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007235-eaab23a7-72fa-4c68-9a16-8dc711c18412.png" alt="Image" width="100" height="100"></td>
    <td>Summon a large anti-gravity array. After a delay, it explodes, <br>launching enemies and dealing 400% damage.<br><br><br><br><br>[Ranged]</td>
    <td>Proc: 1.<br>CD: 10s.</td>
  </tr>
  <tr>
    <td>Beam</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007271-e7f251c0-c430-4347-8a07-f8b494e42ba6.png" alt="Image" width="100" height="100"></td>
    <td>Hold the button to shoot a devastating beam,<br>piercing and dealing 200% damage per tick.<br>The beam explodes on hit, dealing 200% damage to nearby enemies.<br><br><br><br>[Ranged] [RightHanded]</td>
    <td>Proc: 1.<br>CD: 1s.</td>
  </tr>
  <tr>
    <td>Void<br>Missiles</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007256-47fc7a47-14ce-4826-b4fb-65fa5f7407af.png" alt="Image" width="100" height="100"></td>
    <td>Shoot 2x8 homing missiles, dealing 100% damage per missile.<br><br><br><br><br>[Ranged] [RightHanded]</td>
    <td>Proc: 1.<br>CD: 6s.</td>
  </tr>
  <tr>
    <td>Throw<br>Thqwibs</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007232-7b7f5ec5-77cc-4d91-98db-18783125e8fc.png" alt="Image" width="100" height="100"></td>
    <td>Throw 3 thqwibs that activate On-Kill effects and deal 400% damage each.<br><br><br><br><br>[Ranged] [RightHanded]</td>
    <td>Proc: 1.<br>CD: 7s.</td>
  </tr>
  <tr>
    <td>Elementality</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007279-d5ac275f-7ce7-4933-a59f-bee18d68f928.png" alt="Image" width="100" height="100"></td>
    <td>Stealing Artificer's quirk grants Elementality- granting 3 elemental attacks.<br>After using the skill, cycle to the next element. <br>Fire -&gt; Ice -&gt; Lightning.</td>
    <td></td>
  </tr>
  <tr>
    <td>Elementality:<br>Fire</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/169811293-57902aad-80f4-4167-a7b2-77550d2be036.png" alt="Image" width="100" height="100"></td>
    <td>Burn all enemies in front of you for 1500% damage.<br>Cycle to Elementality: Ice.<br><br><br><br>[Ranged] [LeftHanded]</td>
    <td>Proc: 1.<br>CD : 6s</td>
  </tr>
  <tr>
    <td>Elementality:<br>Ice</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/169811301-32a5f0f1-1e41-4ae9-9e0a-a9dd58897b18.png" alt="Image" width="100" height="100"></td>
    <td>Create a barrier that freezes enemies for 100% damage.<br>Cycle to Elementality: Lightning.<br><br><br><br>[Ranged] [LeftHanded]</td>
    <td>Proc: 1.<br>CD : 4s</td>
  </tr>
  <tr>
    <td>Elementality:<br>Lightning</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/169811307-4133d198-a166-4191-a3d6-580f82ceab4b.png" alt="Image" width="100" height="100"></td>
    <td>Charge up an exploding nano-bomb that deals 400%-1200% damage.<br>Cycle to Elementality: Fire.<br><br><br><br>[Ranged] [LeftHanded]</td>
    <td>Proc: 1.<br>CD : 4s</td>
  </tr>
  <tr>
    <td>Lights Out</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007280-1da02e37-e4ea-400f-b5f0-8ca2ef17bf71.png" alt="Image" width="100" height="100"></td>
    <td>Cloak yourself for 3 seconds and ready a shot while holding the button.<br>Release to fire the shot for 300% damage.<br>Kills reset all your cooldowns.<br><br><br>[Ranged] [RightHanded]</td>
    <td>Proc: 1.<br>CD : 4s</td>
  </tr>
  <tr>
    <td>Turret</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007287-0872fa07-e8be-4d26-8090-dd04b510ed32.png" alt="Image" width="100" height="100"></td>
    <td>Place a turret that inherits all your items. Fires a cannon for 100% damage.<br>Can place up to 2.<br><br><br><br>[RightHanded]</td>
    <td>CD : 45s<br></td>
  </tr>
  <tr>
    <td>Flurry</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007290-2e4bab8b-1399-4064-af1e-dd5a0927dca1.png" alt="Image" width="100" height="100"></td>
    <td>Fire 3 seeking arrows at the target for 3x100% damage.<br>Critical strikes fire 6 arrows.<br><br><br><br>[Ranged] [RightHanded]</td>
    <td>Proc: 0.7.<br>CD : 0s</td>
  </tr>
  <tr>
    <td>Eviscerate</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007295-35847b7c-5bb8-44ef-afae-61645f8a63ae.png" alt="Image" width="100" height="100"></td>
    <td>Target the nearest enemy, attacking them for 100% damage repeatedly.<br>You cannot be hit for the duration.<br><br><br><br>[Melee] [LeftHanded]</td>
    <td>Proc: 1.<br>CD : 6s</td>
  </tr>
  <tr>
    <td>Power<br>Stance</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007297-807b2ce3-03cf-4b31-994c-9f8dab8c77d0.png" alt="Image" width="100" height="100"></td>
    <td>Adopt a stance and gain 100 armor, 2x attackspeed but have 0.4x movespeed.<br><br><br><br><br>[RightHanded]</td>
    <td>Proc: 1.<br>CD : 1s</td>
  </tr>
  <tr>
    <td>Cryocharged<br>Railgun</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007298-f584a2b2-630e-42e1-ab88-bced6c6564af.png" alt="Image" width="100" height="100"></td>
    <td>Hold to ready a freezing, piercing round.<br>Release to fire the round for 1500% damage.<br><br><br><br>[Ranged] [LeftHanded]</td>
    <td>Proc: 1.<br>CD : 10s</td>
  </tr>
  <tr>
    <td>Seed <br>Barrage</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007300-e0bd5434-546b-4634-be80-27bd20d6510a.png" alt="Image" width="100" height="100"></td>
    <td>Costs 10% health.<br>Launch a mortar into the sky for 400% damage.<br><br><br><br>[Ranged] [LeftHanded]</td>
    <td>Proc: 1.<br>CD : 0.5s</td>
  </tr>
  <tr>
    <td>Cleanse</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/169811343-adca3a08-930e-4256-88a8-47af74237651.png" alt="Image" width="100" height="100"></td>
    <td>Disappear into the Void, cleansing all debuffs.<br><br><br><br>[LeftHanded]</td>
    <td>CD : 5s<br></td>
  </tr>
</tbody>
</table>

### Passive Skills
<table>
<thead>
  <tr>
    <th>Passive</th>
    <th>Quirk User</th>
    <th>Description</th>
  </tr>
</thead>
<tbody>
  <tr>
    <td>Barrier</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007151-233450f3-4975-4ab8-bd0c-cdfb5b0684d8.png" width="100" height="100"></td>
    <td>Gain a barrier that blocks the next attack.<br>Recharges after 10 seconds.</td>
  </tr>
  <tr>
    <td>Strength <br>Boost</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007152-1aa4ec94-ce67-4cc9-a032-cbdf986f06e9.png" width="100" height="100"></td>
    <td>Gain 1.5x Damage.<br>All [Melee] attacks deal 1.5x damage.<br><br><br><br>[Melee]</td>
  </tr>
  <tr>
    <td>Jump<br>Boost</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007165-196f4f43-7059-4bf1-87fb-caefa25b807b.png" width="100" height="100"></td>
    <td>Gain 4 extra jumps and jump power.<br><br><br><br><br>[Jump]</td>
  </tr>
  <tr>
    <td>Super<br>Speed</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007167-b658b0a1-e18d-406a-97f0-e31ccb2e36f2.png" width="100" height="100"></td>
    <td>Gain 1.5x movespeed.<br>Gain 1.5x sprint speed.<br><br><br><br>[Movespeed]</td>
  </tr>
  <tr>
    <td>Spiky<br>Body</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007193-20146e74-3579-4282-b029-f56dc9c745bc.png" width="100" height="100"></td>
    <td>Gain spikes that deal 150% damage to those around you when you're hit.<br><br><br><br><br>[Melee] [Decay]</td>
  </tr>
  <tr>
    <td>Acid<br>Jump</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007208-d1ed63ea-8965-4e6d-aae8-47593180f635.png" width="100" height="100"></td>
    <td>Release an Acidic blast when jumping or landing.<br>Scales based off jump power.<br><br><br><br>[Melee] [Jump] [Decay] </td>
  </tr>
  <tr>
    <td>Ranged<br>Boost</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007216-4788f02b-2f6b-48e6-9f46-f180e49d717c.png" width="100" height="100"></td>
    <td>Gain 1.5x Damage.<br>All [Ranged] attacks deal 1.5x damage.<br><br><br><br>[Ranged]</td>
  </tr>
  <tr>
    <td>Lunar<br>Aura</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007217-47dd6d9d-1520-4669-9686-3710368a094b.png" width="100" height="100"></td>
    <td>At &lt;50% health, periodically release a lunar blaze.<br>The lunar blaze does 100% damage per tick to enemies on the ground.<br><br><br></td>
  </tr>
  <tr>
    <td>Mortar</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007196-40a60e2f-fe02-4d2a-ba25-24cdc91f22e6.png" width="100" height="100"></td>
    <td>While standing still, attack nearby enemies for 100% damage and<br>gain 1 armor every 1/(Attackspeed) second(s).<br>Radius and Damage scales with armor and attackspeed.<br><br><br>[Mortar]</td>
  </tr>
  <tr>
    <td>Healing<br>Aura</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007224-a670a153-4bfd-4e67-a26e-3ba191625087.png" width="100" height="100"></td>
    <td>Heal yourself and nearby allies 5% health every second.<br><br><br><br><br>[Healing] </td>
  </tr>
  <tr>
    <td>Solus<br>Boost</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007241-9f3b61cf-b4bd-4fb7-aa65-c1df00a67b43.png" width="100" height="100"></td>
    <td>While holding any skill button, gain 2% attack speed per second.</td>
  </tr>
  <tr>
    <td>Void<br>Mortar</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007253-c325d404-8b25-4a8c-9fa4-199c89996d0c.png" width="100" height="100"></td>
    <td>While standing still, attack nearby enemies for 100% damage and<br>gain 0.05 attackspeed every 1/(CurrentArmor/BaseArmor) second(s).<br>Radius and Damage scales with armor and attackspeed.<br><br><br>[Mortar]</td>
  </tr>
  <tr>
    <td>Gravity</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007260-c17830ca-89ea-465f-be43-83b26f839adf.png" width="100" height="100"></td>
    <td>While moving, Pull nearby enemies and deal 100% damage.<br>The gap between attacks scales with movespeed.<br><br><br><br>[Movespeed]</td>
  </tr>
  <tr>
    <td>Bleed</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007202-63b83ac8-e236-4faf-9493-b4bc80f34fea.png" width="100" height="100"></td>
    <td>Attacks apply Bleed.<br><br><br><br><br>[Bleeding]</td>
  </tr>
  <tr>
    <td>Stone<br>Skin</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007249-7b3f655a-63df-4d92-8ee2-5994704b1dec.png" width="100" height="100"></td>
    <td>Gain 10 armor and flat damage reduction equal to your armor.<br>Take no knockback from attacks.<br>At &lt;50% health, damage can be reduced below zero and heal you.<br><br><br>[Healing]</td>
  </tr>
  <tr>
    <td>Blazing<br>Aura</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007222-ea28b2fd-f179-4e85-837e-da0f376a19dc.png" width="100" height="100"></td>
    <td>Burn nearby enemies for 150% damage every 1/Attackspeed seconds.</td>
  </tr>
  <tr>
    <td>Lightning<br>Aura</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007228-61bc7ae7-53af-43f5-9a42-b08d08a592a6.png" width="100" height="100"></td>
    <td>Summon lightning bolts on nearby enemies for <br>500% damage every 1/Attackspeed seconds.</td>
  </tr>
  <tr>
    <td>Vagrant's<br>Orb</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007268-355cbb10-596a-46e7-b5b7-0ddfc1cbc1e7.png" width="100" height="100"></td>
    <td>When striking an enemy for &gt;= 400% damage,<br>Create a Nova explosion that stuns and deals 700% damage. <br> </td>
  </tr>
  <tr>
    <td>Poison</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007278-6916e4b3-0177-4fc3-b5f0-6fd4c3518cbf.png" alt="Image" width="100" height="100"></td>
    <td>Attacks apply Poison.<br><br><br><br><br>[Poison]</td>
  </tr>
  <tr>
    <td>Double<br>Tap</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007285-f812da3b-c6a4-41ed-a258-08d2afe33ec8.png" alt="Image" width="100" height="100"></td>
    <td>All attacks hit twice, dealing 10% damage of the attack,<br>with a proc coefficient of 1.</td>
  </tr>
  <tr>
    <td>Defensive<br>Microbots</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007282-dab70704-0cd4-4935-bbfe-818e5c537970.png" alt="Image" width="100" height="100"></td>
    <td>Passively gain Microbots that shoot down nearby enemy projectiles.<br>Drones are also given Microbots.</td>
  </tr>
  <tr>
    <td>Scrap<br>Barrier</td>
    <td><img src="https://user-images.githubusercontent.com/93917577/168007292-79a3d5b1-537f-463a-8d30-fff04f879c6a.png" alt="Image" width="100" height="100"></td>
    <td>Gain 5% of your max health as barrier <br>and 1.5x damage on all [Melee] attacks.<br><br><br>[Melee]</td>
  </tr>
</tbody>
</table>


## Numbers
##### Armor = 10 + 0.5 per level
##### Damage = 5 + 1 per level
##### Regen = 1 + 0.2 per level 
##### Health = 141 + 41 per level
##### Movespeed = 7

These stats are prone to change.

rest of changelog on github.
## Changelog

<details>
<summary>Click to expand previous patch notes:</summary>

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

## Credits
##### HenryMod for the template.
  
## OG Pictures
![128x128Icon](https://user-images.githubusercontent.com/93917577/168004591-39480a52-c7fe-4962-997f-cd9460bb4d4a.png)
![Character Select](https://github.com/1TeaL/Shigaraki/assets/93917577/5682234e-9b60-489d-9f36-2a35f060fa0e)

![Passive](https://github.com/1TeaL/Shigaraki/assets/93917577/07984117-9535-4e63-af1b-7b5e1367a3b3)

![All For One](https://github.com/1TeaL/Shigaraki/assets/93917577/94c409bd-fe00-4e02-bbcb-ac8c4bdf5e13)

![Decay](https://github.com/1TeaL/Shigaraki/assets/93917577/ab967a05-a700-48ec-9ce0-623b6a2a3f00)
![Bulle tLaser](https://github.com/1TeaL/Shigaraki/assets/93917577/162d0caf-8cb6-4bd6-ac8f-431bf6e0bf81)
![Air Cannon](https://github.com/1TeaL/Shigaraki/assets/93917577/ce421232-8fa7-4299-8596-419f8e79473f)
![Multiplier](https://github.com/1TeaL/Shigaraki/assets/93917577/f28564ff-4b14-40cb-87ee-6b83a7b79c83)

![Barrier](https://github.com/1TeaL/Shigaraki/assets/93917577/ef357abb-4701-4977-948c-0345282f3604)
![Strength Boost](https://github.com/1TeaL/Shigaraki/assets/93917577/db4db6e5-a93e-4634-9de4-92d533ce5672)
![Jump Boost](https://github.com/1TeaL/Shigaraki/assets/93917577/f798ede9-2f28-4b00-9f4d-b0e20a0fc6dd)
![Super Speed](https://github.com/1TeaL/Shigaraki/assets/93917577/7184fd02-d366-4ac9-9308-83f58ced65ea)
![Spiky Body](https://github.com/1TeaL/Shigaraki/assets/93917577/dd7974a7-f9da-4aa0-9177-2c9618a5578e)
![Mortar](https://github.com/1TeaL/Shigaraki/assets/93917577/90604021-91a5-4b60-9f72-51906a25b7b7)
![Acid Jump](https://github.com/1TeaL/Shigaraki/assets/93917577/98b4c3c3-5e08-41f9-807c-173c3236da1d)
![Haste](https://github.com/1TeaL/Shigaraki/assets/93917577/4414c0f0-7c40-4f44-b4af-f2aa8d590736)
![Lunar Barrier](https://github.com/1TeaL/Shigaraki/assets/93917577/4f829eb0-2e5a-48a1-bc98-5981a74448bd)
![Healing Aura](https://github.com/1TeaL/Shigaraki/assets/93917577/16bbe7da-9e14-43d3-b28f-a2da8289b4e1)
![Solus Boost](https://github.com/1TeaL/Shigaraki/assets/93917577/7e75b5db-4dad-48ba-a16d-9a9fcdf2f7d8)
![Void Mortar](https://github.com/1TeaL/Shigaraki/assets/93917577/932ca55c-2cdf-4c98-81a4-60f3c31174e0)
![Gravity](https://github.com/1TeaL/Shigaraki/assets/93917577/53c020c7-51dd-4e4a-9e8b-ac6cdbc905df)
![Bleed](https://github.com/1TeaL/Shigaraki/assets/93917577/102c2a3a-2a0c-4439-95cc-ec25e82f202e)
![Stone Skin](https://github.com/1TeaL/Shigaraki/assets/93917577/b81b4fc1-0977-4404-8eac-b6a0731c2e03)
![Blazing Aura](https://github.com/1TeaL/Shigaraki/assets/93917577/102556a2-ae3f-463c-b071-c7e8e1724ef4)
![Lightning Aura](https://github.com/1TeaL/Shigaraki/assets/93917577/b06db688-0df0-4ef6-bd5b-5cab0413464c)
![Vagrant's Orb](https://github.com/1TeaL/Shigaraki/assets/93917577/fd9244d0-5dcc-44f3-af97-aeef0f9cec0e)
![Poison](https://github.com/1TeaL/Shigaraki/assets/93917577/333cc819-2242-48ec-91af-a4e8e42093af)
![Double Tap](https://github.com/1TeaL/Shigaraki/assets/93917577/986ed68e-d714-430f-9589-de35cdae981c)
![Defensive Microbots](https://github.com/1TeaL/Shigaraki/assets/93917577/2de8abd1-a166-4b41-a167-3fb9fb031d90)
![Scrap Barrier](https://github.com/1TeaL/Shigaraki/assets/93917577/d0a9c072-539a-4be0-98b8-c03c839747f6)
![Big Bang](https://github.com/1TeaL/Shigaraki/assets/93917577/6916edd7-d089-40fa-8ba3-8c21cca3b5fd)
![Wisper](https://github.com/1TeaL/Shigaraki/assets/93917577/7c0b1702-b459-4b54-b7c2-08ef3dd5658a)
![Omniboost](https://github.com/1TeaL/Shigaraki/assets/93917577/9d13e6ba-9d6f-4673-9278-c33461cfb12a)
![Gacha](https://github.com/1TeaL/Shigaraki/assets/93917577/59737b09-8720-4a7e-a22e-db64d479b4fd)
![Stone Form](https://github.com/1TeaL/Shigaraki/assets/93917577/d888da24-869f-4b51-9768-d95ac6c92e1d)
![Aura Of Blight](https://github.com/1TeaL/Shigaraki/assets/93917577/254b9077-b72b-45dd-9824-0621aee751fd)
![Barbed Spikes](https://github.com/1TeaL/Shigaraki/assets/93917577/634b51bc-d435-44b2-b56b-a9fe014d5b66)
![Ingrain](https://github.com/1TeaL/Shigaraki/assets/93917577/e7422797-279b-4fef-8ca2-16cad757ee5b)
![Double Time](https://github.com/1TeaL/Shigaraki/assets/93917577/87164301-536f-4cfa-aeb3-f445855f4ddd)
![Blind Sense](https://github.com/1TeaL/Shigaraki/assets/93917577/c1a1a333-6b40-467e-a9b3-531437d0d472)
![Supernova](https://github.com/1TeaL/Shigaraki/assets/93917577/d458ef84-0da6-431e-9eda-639b4302f72b)
![Reversal](https://github.com/1TeaL/Shigaraki/assets/93917577/a91f8b0f-c769-47f1-a59a-773c36fdcdb0)
![Machine Form](https://github.com/1TeaL/Shigaraki/assets/93917577/ae3b9ae1-8f60-4325-924f-a32a8be5f76e)
![Gargoyle Protection](https://github.com/1TeaL/Shigaraki/assets/93917577/764a4d17-57f0-4b33-a3f6-de66be349214)
![Weather Report](https://github.com/1TeaL/Shigaraki/assets/93917577/2e4884f3-86fa-4733-b46e-3fd1239191be)
![Decay Awakened](https://github.com/1TeaL/Shigaraki/assets/93917577/947f59fc-573c-43a6-91bd-a9be388bccc2)

![Wind Blast](https://github.com/1TeaL/Shigaraki/assets/93917577/6e722287-a9d7-4ee0-8810-0a4a301a5f1a)
![Fast Drop](https://github.com/1TeaL/Shigaraki/assets/93917577/a9d913a5-cef8-4e0d-9c0c-55c22ece0a59)
![Charging](https://github.com/1TeaL/Shigaraki/assets/93917577/636fd38f-16cd-480d-a241-a3f2b8092989)
![Spiked Ball Control](https://github.com/1TeaL/Shigaraki/assets/93917577/c9396a83-d104-46ec-af21-d2da1e56b649)
![Clay AirStrike](https://github.com/1TeaL/Shigaraki/assets/93917577/9894e4dd-cd6d-45f9-8b17-53a8441376b7)
![Clay Minigun](https://github.com/1TeaL/Shigaraki/assets/93917577/19285a31-19d3-4294-a623-4e263bb7330e)
![Fire Blast](https://github.com/1TeaL/Shigaraki/assets/93917577/7e23789b-03f1-4a0f-b759-1238b8bba31e)
![Spirit Boost](https://github.com/1TeaL/Shigaraki/assets/93917577/1b4cf9ce-6ff7-4c5b-a127-53219a887e19)
![Blink](https://github.com/1TeaL/Shigaraki/assets/93917577/0191460d-34cc-4fa2-827c-1e529241b6d8)
![Regenerate](https://github.com/1TeaL/Shigaraki/assets/93917577/30b8a593-05ac-4477-a540-fd337d08f922)
![Fireball](https://github.com/1TeaL/Shigaraki/assets/93917577/07622e9a-162f-4c52-8025-cf7b235354c9)
![Slide Reset](https://github.com/1TeaL/Shigaraki/assets/93917577/b142513a-c328-40f8-8085-3c22cfb72f23)
![Lunar Minigun](https://github.com/1TeaL/Shigaraki/assets/93917577/f1191f39-550f-4a6a-8996-eb2485d891f2)
![Teleport](https://github.com/1TeaL/Shigaraki/assets/93917577/6d4c5c8a-7362-411d-8332-a4385d7a99b4)
![Laser](https://github.com/1TeaL/Shigaraki/assets/93917577/15003f12-97bb-4e26-871f-ddc3ca154b4a)
![Nullifier Artillery](https://github.com/1TeaL/Shigaraki/assets/93917577/b8ee0bc1-6c14-44e4-b70b-4646299e0956)
![Summon Ally](https://github.com/1TeaL/Shigaraki/assets/93917577/0ddfeac8-141d-4fc3-b5e5-c3fa9e5a3d0c)
![Solar Flare](https://github.com/1TeaL/Shigaraki/assets/93917577/fe4f016b-97ee-4ebc-89b7-75ea398560f6)
![Chain](https://github.com/1TeaL/Shigaraki/assets/93917577/41614b1b-e528-4b28-8cb4-d93caa4fe880)
![Tar Boost](https://github.com/1TeaL/Shigaraki/assets/93917577/a232cace-39b2-4192-868a-7c543ff4cbb9)
![Anti Gravity](https://github.com/1TeaL/Shigaraki/assets/93917577/f726c847-e358-46c3-880d-2a33cc6bfe73)
![Beam](https://github.com/1TeaL/Shigaraki/assets/93917577/122c06ad-2318-47c2-b1e8-76faf72627d2)
![Void Missiles](https://github.com/1TeaL/Shigaraki/assets/93917577/4e325b79-6aa1-4125-a16b-b75218fe0e02)
![Throw Thqwibs](https://github.com/1TeaL/Shigaraki/assets/93917577/b0c39263-f56b-4cda-ac37-c566e6a27aab)
![Elementality: Fire](https://github.com/1TeaL/Shigaraki/assets/93917577/44cf45b6-5a4a-40d2-bf8a-1575d8ebc159)
![Elementality: Ice](https://github.com/1TeaL/Shigaraki/assets/93917577/ae5ffc8c-76b3-4a55-a57a-2aa46bdd0793)
![Elementality: Lightning](https://github.com/1TeaL/Shigaraki/assets/93917577/505d2067-4ae9-498a-b156-7583878336e9)
![Lights Out](https://github.com/1TeaL/Shigaraki/assets/93917577/c3230348-c486-4c69-9e4d-1e1239c99896)
![Turret](https://github.com/1TeaL/Shigaraki/assets/93917577/b7c99e5f-b28f-410e-a9d6-56566bd5b9f7)
![Flurry](https://github.com/1TeaL/Shigaraki/assets/93917577/b420ad59-98c6-4f71-a2c8-432d3dc3b700)
![Wind Assault](https://github.com/1TeaL/Shigaraki/assets/93917577/d8aef2db-c148-49bb-ad7d-d30538354126)
![Power Stance](https://github.com/1TeaL/Shigaraki/assets/93917577/ef4433cd-5223-4cf8-ada7-6345bb0b25e7)
![Cryocharged Railgun](https://github.com/1TeaL/Shigaraki/assets/93917577/762077c9-3d64-44d6-8f30-81f5c8009bfb)
![Seed Barrage](https://github.com/1TeaL/Shigaraki/assets/93917577/fa1b9302-381c-4723-bcce-494db447a95a)
![Cleanse](https://github.com/1TeaL/Shigaraki/assets/93917577/d8262edd-07ba-44e1-aa9e-20e424bcd23a)
![One For All](https://github.com/1TeaL/Shigaraki/assets/93917577/80a802a6-d429-4eed-b5da-eaa56b86bb93)
![Sweeping Beam](https://github.com/1TeaL/Shigaraki/assets/93917577/c6e957d8-73e1-43d2-a04c-b775b59c3288)
![Blackhole Glaive](https://github.com/1TeaL/Shigaraki/assets/93917577/aaaf8d33-9f38-46a9-8980-4dc1b4affa93)
![Gravitational Downforce](https://github.com/1TeaL/Shigaraki/assets/93917577/f9572275-69da-49f3-bfdf-d4b8b8142d7f)
![Wind Shield](https://github.com/1TeaL/Shigaraki/assets/93917577/cb1487fe-b16d-4745-bf80-01d3ef3064f9)
![Genesis](https://github.com/1TeaL/Shigaraki/assets/93917577/6e8b4ffc-0a39-42df-8e2d-11397582c86e)
![Refresh](https://github.com/1TeaL/Shigaraki/assets/93917577/1cf9758a-f384-4884-8d78-9dd7ec04964f)
![Expunge](https://github.com/1TeaL/Shigaraki/assets/93917577/cea4a457-b33e-45e1-bf61-4e6eaba9330c)
![Shadow Claw](https://github.com/1TeaL/Shigaraki/assets/93917577/fee8a053-d3e2-4efd-92f7-d32dce1ffac5)
![Orbital Strike](https://github.com/1TeaL/Shigaraki/assets/93917577/bc3044ee-28bb-4683-a2c9-d20b6640b7ee)
![Thunderclap](https://github.com/1TeaL/Shigaraki/assets/93917577/352aa28a-6035-4b6d-95d2-c6a6aafd83e2)
![Blast Burn](https://github.com/1TeaL/Shigaraki/assets/93917577/7342948a-4885-41f9-892b-4690762893a6)
![Barrier Jelly](https://github.com/1TeaL/Shigaraki/assets/93917577/582d6c1f-4e36-449e-95f9-4fa57c450222)
![Mech Stance](https://github.com/1TeaL/Shigaraki/assets/93917577/ebc4a8a3-8f9f-455d-ab68-ef6c086bbdbc)
![Wind Slash](https://github.com/1TeaL/Shigaraki/assets/93917577/de9925fe-b68e-4e6c-9c2a-2f23f2d67f6e)
![Limit Break](https://github.com/1TeaL/Shigaraki/assets/93917577/6c5dcc2c-3cb3-4619-b19e-af397aa33c85)
![Void Form](https://github.com/1TeaL/Shigaraki/assets/93917577/2809d190-1f92-4559-a7b0-72aaeceeaeb4)
![Elemental Fusion](https://github.com/1TeaL/Shigaraki/assets/93917577/49657047-ef82-4d32-88da-6cff2fdc48fe)
![Decay Plus Ultra](https://github.com/1TeaL/Shigaraki/assets/93917577/5a93a38b-64d1-4dff-a2b8-d57712a6ac89)
![Mach Punch](https://github.com/1TeaL/Shigaraki/assets/93917577/48e2dd0c-b965-474f-ad0c-dc14234297a6)
![Rapid Pierce](https://github.com/1TeaL/Shigaraki/assets/93917577/e1d3b32e-57ef-49f1-99aa-698c63afe737)
![The World](https://github.com/1TeaL/Shigaraki/assets/93917577/20d3c2a0-24e5-4341-9ae8-36474542965e)
![Extreme Speed](https://github.com/1TeaL/Shigaraki/assets/93917577/73871ac0-8ae3-4a09-8e01-9d4dc382cdd0)
![Death Aura](https://github.com/1TeaL/Shigaraki/assets/93917577/c60ebe30-a116-400c-b575-cdb7cb828c26)
![One For All For One](https://github.com/1TeaL/Shigaraki/assets/93917577/8d052de9-8dfa-494b-b36b-45536fe93438)
![X Beamer](https://github.com/1TeaL/Shigaraki/assets/93917577/3ab7ef7f-3acf-48e2-8111-3af97b314f3f)
![Final Release](https://github.com/1TeaL/Shigaraki/assets/93917577/3e6afd33-fb37-4cb6-b21a-23f4063228ea)
![Blasting Zone](https://github.com/1TeaL/Shigaraki/assets/93917577/de446fa8-97f7-4717-b4d9-cb9782412d43)
![Wild Card](https://github.com/1TeaL/Shigaraki/assets/93917577/33f73285-06f6-4708-83cb-8553a9cdc821)
![Light And Darkness](https://github.com/1TeaL/Shigaraki/assets/93917577/0349003a-5618-48e2-9560-76299538fd16)



![Stone_Titan](https://github.com/1TeaL/Shigaraki/assets/93917577/7c013b34-3a98-4e35-8e4f-b41e4723d16b)
![Void_Barnacle](https://github.com/1TeaL/Shigaraki/assets/93917577/f2465f1b-f9c0-4720-a1b6-6fcd5ecd6aa6)
![Void_Devastator](https://github.com/1TeaL/Shigaraki/assets/93917577/ccc339f6-a111-4070-ab76-a73469fcd2ff)
![Void_Fiend](https://github.com/1TeaL/Shigaraki/assets/93917577/3b5c9503-dd00-48b7-b7a7-bfa264b3b9fb)
![Void_Jailer](https://github.com/1TeaL/Shigaraki/assets/93917577/c7e07b27-b10a-4ee7-9d2c-4e50897811fd)
![Void_Reaver](https://github.com/1TeaL/Shigaraki/assets/93917577/b204ebc9-4776-4e73-8555-7f46dc3a95c2)
![Wandering_Vagrant](https://github.com/1TeaL/Shigaraki/assets/93917577/1d4a965b-c3b7-4f59-ac4a-0b42bda8e856)
![Xi_Construct](https://github.com/1TeaL/Shigaraki/assets/93917577/4b7a0c36-8f70-4d26-beef-80a95e54cf6a)
![Acrid](https://github.com/1TeaL/Shigaraki/assets/93917577/d1799419-c4dd-4afe-b17a-6283d2f587ae)
![aircannon](https://github.com/1TeaL/Shigaraki/assets/93917577/fcd78531-cd2d-47f0-b4ff-21e2d7024cfa)
![Alloy_Vulture](https://github.com/1TeaL/Shigaraki/assets/93917577/0e7d6edb-b086-48dd-9145-9587a8a8e70b)
![Alpha_Construct](https://github.com/1TeaL/Shigaraki/assets/93917577/4f0497ef-cb2e-403d-b1e3-694b3a8130ba)
![ArtificerFire](https://github.com/1TeaL/Shigaraki/assets/93917577/eac8e346-67b4-47bb-bf03-dab1e1dc6a63)
![ArtificerIce](https://github.com/1TeaL/Shigaraki/assets/93917577/9275b5d4-b0b5-41de-8108-ae5fb8cad0a6)
![ArtificerLightning](https://github.com/1TeaL/Shigaraki/assets/93917577/e596c543-e3dc-426f-97bf-e4bcd0ff7b1d)
![Bandit](https://github.com/1TeaL/Shigaraki/assets/93917577/e63a602f-c58f-4882-a760-e19548af54eb)
![Beetle](https://github.com/1TeaL/Shigaraki/assets/93917577/a31cb5fc-f562-435a-9aa8-eb4ef2ce16e8)
![Beetle_Guard](https://github.com/1TeaL/Shigaraki/assets/93917577/7815bbcd-0194-4f6b-8c6f-abe13f7ed5cb)
![Beetle_Queen](https://github.com/1TeaL/Shigaraki/assets/93917577/8821d06d-4720-4837-946e-f6ad61f5bb5c)
![Bison](https://github.com/1TeaL/Shigaraki/assets/93917577/72c729a9-5504-42f6-926f-c557d39114cd)
![Blind_Pest](https://github.com/1TeaL/Shigaraki/assets/93917577/1d21b065-63e2-4107-b869-40e4d489426e)
![Blind_Vermin](https://github.com/1TeaL/Shigaraki/assets/93917577/d837bf56-dc16-473a-983f-71f1cc33f657)
![Brass_Contraption](https://github.com/1TeaL/Shigaraki/assets/93917577/3e8009ce-15a2-44d5-aa23-2c0094e578c1)
![bulletlaser](https://github.com/1TeaL/Shigaraki/assets/93917577/9fe711c8-ff4d-4e7e-b17b-1d9bce539a33)
![Captain](https://github.com/1TeaL/Shigaraki/assets/93917577/b964944f-f518-4d9e-a2c0-48df1a296dc7)
![Clay_Apothecary](https://github.com/1TeaL/Shigaraki/assets/93917577/defb5778-6a7f-40af-8e57-00ea9fa9fc38)
![Clay_Dunestrider](https://github.com/1TeaL/Shigaraki/assets/93917577/aad79312-d8c1-4ead-8a59-a437d3440202)
![Clay_Templar](https://github.com/1TeaL/Shigaraki/assets/93917577/24ec5951-df39-43d7-aba5-8ed69e63e441)
![Commando](https://github.com/1TeaL/Shigaraki/assets/93917577/3eacffab-55b4-40cd-8afd-0518a9cc28f4)
![decay](https://github.com/1TeaL/Shigaraki/assets/93917577/17065389-40ee-4920-b1f3-4c0a1d82c5a1)
![Elder_Lemurian](https://github.com/1TeaL/Shigaraki/assets/93917577/f08193e8-c036-40d6-a387-ef1d09d61427)
![Engineer](https://github.com/1TeaL/Shigaraki/assets/93917577/584b19d3-a50c-4c1b-974e-449d13f7cd24)
![Grandparent](https://github.com/1TeaL/Shigaraki/assets/93917577/332fb077-bacc-4234-bc05-7ccb86aa667d)
![Greater_Wisp](https://github.com/1TeaL/Shigaraki/assets/93917577/3c6164fa-e859-4e34-ba42-f3294996b7e2)
![Grovetender](https://github.com/1TeaL/Shigaraki/assets/93917577/6a31c895-a997-436a-a51f-512b942c08a4)
![Gup](https://github.com/1TeaL/Shigaraki/assets/93917577/37ec2413-8b1b-4df8-91a0-1d169a8761e2)
![Hermit_Crab](https://github.com/1TeaL/Shigaraki/assets/93917577/a02a9653-bc3e-415a-960d-d45d1f9a124a)
![Huntress](https://github.com/1TeaL/Shigaraki/assets/93917577/8bd7a9fa-dee9-49e2-8292-7347ba3e87f6)
![Imp](https://github.com/1TeaL/Shigaraki/assets/93917577/cd0cb5f1-8090-4fa9-bea3-232bfc97062c)
![Imp_Overlord](https://github.com/1TeaL/Shigaraki/assets/93917577/6cf1ba24-4be4-4d05-be58-127e046bf1a4)
![Jellyfish](https://github.com/1TeaL/Shigaraki/assets/93917577/c1ad1a3f-b56b-4b0d-8e21-02b911fb41b9)
![Larva](https://github.com/1TeaL/Shigaraki/assets/93917577/847d7f1c-5115-49f8-8fec-e4e4f5ed72dc)
![Lemurian](https://github.com/1TeaL/Shigaraki/assets/93917577/95bb7456-b5d9-45e9-9e99-de41ab1f60d4)
![Lesser_Wisp](https://github.com/1TeaL/Shigaraki/assets/93917577/7df73435-6afb-45d8-90b5-0204bc60758a)
![Loader](https://github.com/1TeaL/Shigaraki/assets/93917577/49a0095c-ba3c-44c0-9476-5a86f2983f42)
![Lunar_Exploder](https://github.com/1TeaL/Shigaraki/assets/93917577/7a39fe19-842b-44ed-b1b7-2926b54917b4)
![Lunar_Golem](https://github.com/1TeaL/Shigaraki/assets/93917577/9005930d-cd98-483f-ac0c-0c1db625910b)
![Lunar_Wisp](https://github.com/1TeaL/Shigaraki/assets/93917577/4b8402da-d3bc-4b84-8fc7-145a887d1f56)
![Magma_Worm](https://github.com/1TeaL/Shigaraki/assets/93917577/de847f56-2c25-49c2-9693-03d80a9fc585)
![Mercenary](https://github.com/1TeaL/Shigaraki/assets/93917577/6d921a98-96ec-45f0-ac82-5aa10bc55971)
![Mini_Mushrum](https://github.com/1TeaL/Shigaraki/assets/93917577/116230db-9382-4677-a43c-a09f33c2aec0)
![MUL-T](https://github.com/1TeaL/Shigaraki/assets/93917577/549b6f3d-5e97-476a-9214-02acd138827a)
![multiplier](https://github.com/1TeaL/Shigaraki/assets/93917577/d7e5d79d-9cc2-4448-9618-722336d2a059)
![OFA](https://github.com/1TeaL/Shigaraki/assets/93917577/c71c4f5c-e295-4dd1-b031-ba215dfff60e)
![Overloading_Worm](https://github.com/1TeaL/Shigaraki/assets/93917577/8b688e43-b14d-4e6e-a1b6-0be64231a334)
![Parent](https://github.com/1TeaL/Shigaraki/assets/93917577/637aec18-2719-4c64-963a-c830ad46b652)
![Railgunner](https://github.com/1TeaL/Shigaraki/assets/93917577/f64a9ded-9d7d-4edd-be47-ea8b595d3af1)
![REX](https://github.com/1TeaL/Shigaraki/assets/93917577/227a47b8-52c7-42fc-af0d-c9362430e515)
![Scavenger](https://github.com/1TeaL/Shigaraki/assets/93917577/0985f91a-48c9-4521-a4b9-030c5d6e4eb6)
![Solus_Control_Unit](https://github.com/1TeaL/Shigaraki/assets/93917577/4ca7780c-e985-4e5d-9f35-174ca0c4c280)
![Solus_Probe](https://github.com/1TeaL/Shigaraki/assets/93917577/c878ee21-f8f9-4ce5-b55e-72c5fb5ecb5e)
![Stone_Golem](https://github.com/1TeaL/Shigaraki/assets/93917577/224d1a9a-5fc5-4df3-a345-12034f608a8c)

![XBeamer](https://github.com/1TeaL/Shigaraki/assets/93917577/9bbae256-eb19-46cf-b440-63fa1587dd62)
![Wisper](https://github.com/1TeaL/Shigaraki/assets/93917577/62b92d60-5bcc-4775-b9e7-aab2f26f8089)
![WindSlash](https://github.com/1TeaL/Shigaraki/assets/93917577/7d59fee1-b875-4cd4-a107-91af26a0fad3)
![WindShield](https://github.com/1TeaL/Shigaraki/assets/93917577/0614ca70-0cd1-4c6a-832b-1fec166812d1)
![WildCard](https://github.com/1TeaL/Shigaraki/assets/93917577/ecabe5d5-8917-4966-966b-dca74806f330)
![WeatherReport](https://github.com/1TeaL/Shigaraki/assets/93917577/f29532ef-cc38-45a7-b6d8-c55e57299cd8)
![VoidForm](https://github.com/1TeaL/Shigaraki/assets/93917577/22067772-ae72-464e-9905-ecc245dbd9ef)
![ThunderClap](https://github.com/1TeaL/Shigaraki/assets/93917577/41ba0c85-faf4-45cc-bca3-fa932fd36b67)
![TheWorld](https://github.com/1TeaL/Shigaraki/assets/93917577/7f7cfea5-6b99-4bb1-959f-562fc22883d5)
![SweepingBeam](https://github.com/1TeaL/Shigaraki/assets/93917577/e2a636c6-e06c-46c5-a52e-186ee6cb3787)
![Supernova](https://github.com/1TeaL/Shigaraki/assets/93917577/6c04da62-0898-4d30-b6e5-80c565cb8327)
![StoneForm](https://github.com/1TeaL/Shigaraki/assets/93917577/2c303ea2-004d-4ec7-9b20-6ae7ac91d9bf)
![ShadowClaw](https://github.com/1TeaL/Shigaraki/assets/93917577/d40cef57-dbac-471e-8b92-3f53314ae63a)
![Reversal](https://github.com/1TeaL/Shigaraki/assets/93917577/ab411cd3-2069-49ac-9fd6-a79b4dcad010)
![Refresh](https://github.com/1TeaL/Shigaraki/assets/93917577/6f3b3ec4-a5e9-4c66-a351-e2be5aea0937)
![RapidPierce](https://github.com/1TeaL/Shigaraki/assets/93917577/76f3221b-34db-424d-beef-01534bfe3863)
![OrbitalStrike](https://github.com/1TeaL/Shigaraki/assets/93917577/ec314bc5-e212-4a65-84d4-945ec0eb5957)
![OneForAllForOne](https://github.com/1TeaL/Shigaraki/assets/93917577/627b8f27-3962-4553-9ce9-42d523cf0216)
![Omniboost](https://github.com/1TeaL/Shigaraki/assets/93917577/d7708376-a31d-41ee-ae5d-729d6809f976)
![MechStance](https://github.com/1TeaL/Shigaraki/assets/93917577/a67ec9b2-0968-4fc7-aa3c-c025be916a69)
![MachPunch](https://github.com/1TeaL/Shigaraki/assets/93917577/6703615e-2115-410c-bc38-4fc09ad8a460)
![MachineForm](https://github.com/1TeaL/Shigaraki/assets/93917577/f62d5584-035f-448a-a41e-ce0801a2db6b)
![LimitBreak](https://github.com/1TeaL/Shigaraki/assets/93917577/ec44da29-8e56-402b-b292-b9cf5acfe93d)
![LightAndDarkness](https://github.com/1TeaL/Shigaraki/assets/93917577/8fc06d8d-c0e4-4a43-a657-c13d627ff398)
![Ingrain](https://github.com/1TeaL/Shigaraki/assets/93917577/cc00a142-da72-4fc8-b1d2-ebe528fe11d8)
![GravitationalDownforce](https://github.com/1TeaL/Shigaraki/assets/93917577/b6a79379-c937-4f35-9671-cc8666abbcd7)
![Genesis](https://github.com/1TeaL/Shigaraki/assets/93917577/b01e53a1-6d21-49a8-9ea4-ff89e2647cfa)
![GargoyleProtection](https://github.com/1TeaL/Shigaraki/assets/93917577/e5c985da-ef76-49ed-83af-43d5f68b531d)
![Gacha](https://github.com/1TeaL/Shigaraki/assets/93917577/1a911136-fd29-4f3b-aa51-3ddee26d2a8e)
![FinalRelease](https://github.com/1TeaL/Shigaraki/assets/93917577/f69757d5-7d17-4eda-9931-19541aaa2164)
![ExtremeSpeed](https://github.com/1TeaL/Shigaraki/assets/93917577/1ad12ab0-043b-4da6-83b8-d611c8fc3d87)
![Expunge](https://github.com/1TeaL/Shigaraki/assets/93917577/2442abf8-2261-419b-8d3d-a98496c4605a)
![ElementalFusion](https://github.com/1TeaL/Shigaraki/assets/93917577/1234b2a3-a9ef-484c-b0c2-fd13c8eb3d01)
![DoubleTime](https://github.com/1TeaL/Shigaraki/assets/93917577/21f4560f-0f52-4120-b43e-e594fd251e49)
![DecayPlusUltra](https://github.com/1TeaL/Shigaraki/assets/93917577/aacff2d5-9b0c-4175-887e-f528adbf8ea0)
![DecayAwakened](https://github.com/1TeaL/Shigaraki/assets/93917577/561c736a-1589-4e37-ba1f-3af13ae4f0b2)
![DeathAura](https://github.com/1TeaL/Shigaraki/assets/93917577/67a38706-a0ec-4fbc-b3f5-636a2dee4c47)
































VAR core = 0
VAR needs = 0
VAR bounds = 0
VAR violations = 0
VAR commonalities = 0
VAR YaelTell = 0
VAR medPoints = 0
VAR tension = 6
VAR highTen = false
VAR jasperVocab = false
VAR yaelRitual = false
VAR NotesIndex = 0
VAR YaelExhaust = 0
VAR ExternalTutorialNum = 0

#Yael
Edrick! Edrick! We have to stop. There's hotcake crumbs all over my skirt and it's all this hooligan's fault! 
+[...okay, let's take a breather. #EdrickContinue]
~IncreaseTension(1)
#Jasper
You're the one who grabbed my hand like the cakes were poisonous. And in front of the nice townsfolk, too! You saw that, right Edrick?
++[I did see that. #EdrickContinue]
~IncreaseTension(1)
#Yael
I wouldn't have had to do anything if you knew how to act! 
+++[Okay, okay. we're stopping. #EdrickContinue]
~ExtendedTutorial(1)
~IncreaseTension(1)
#Jasper 
You’re not <b>seriously</b> stopping us for this, are you? We’ve already lost precious time. If we are late for the <color=orange> Ritual </color>, we’ll have set out for nothing. 
+++++[I just want everyone to get along. #EdrickContinue]

#Yael
If Edrick won’t stop us for this, I will. No <color=orange>Fate-struck</color> person should be as ill mannered and gross as you’re acting right now, especially not during the Ritual. What kind of Calibrator are you, Edrick?
++++++[...There won't be a ritual at this rate if you two keep arguing. #EdrickContinue]

#Jasper
I’m ill mannered? You made me drop food onto the ground!
~IncreaseTension(1)
+++++++[Right... #EdrickContinue]

#Yael
I had to act quickly, <i>Jester,</i> before you ruined the food by inhaling it!
~IncreaseTension(1)
++++++++[Okay, okay, calm down... #EdrickContinue]

#Jasper
It’s <b>JASPER.</b> And that’s what you’re supposed to do with food. You're supposed to eat it!
+++++++++[Continue #EdrickContinue]
#Edrick
Calm yourselves. Please confide in me, so I can help you two walk in each others’ shoes.
++++++++++[Continue #EdrickContinue] -> COOLOFF


//COOLOFF SECTION=================================
=COOLOFF
#Edrick
Okay, let's figure this out. 
+[Talk to Yael #EdrickChoice]-> Yael
+[Talk to Jasper #EdrickChoice] ->JASPER
+[Regroup #EdrickChoice]  -> REGROUP
->DONE

=Yael
#Yael
//if all other options are exhausted: 
   {YaelExhaust >= 3:My dear Calibrator, I have nothing left to say to you.}
//regular commentary   
#Yael
   {YaelExhaust < 3:You don't understand, Edrick. She's making us look like idiots out there.}


//Option 1:
+[Never mind. #EdrickChoice] ->COOLOFF
//Option 2: 
*[Why do you think that? #EdrickChoice]
~IncreaseYaelExhaust(1)
#Yael
We are not average people. Not <i>anymore</i>. Our decorum around villagers should reflect that. How can they look up to us as Fate-struck if we’re taking handouts from everyone we cross?
	++[Are they handouts, or offerings of goodwill? #EdrickChoice]
#Yael
What’s the difference? We shouldn’t be so eager to show that we are fragile. They may lose faith in us - <color=blue>it’s better to kindly refuse a gift than to show dependence on it.</color>
	+++[Our gods have chosen us for a reason. Any god fearing person will also have faith in us. #EdrickChoice]
	//~ChangeNotesIndex(2)
	//~UpdateNote()
#YaelPensive
Well, that’s certainly true. Though it does little to ease my nerves.
	++++[It sounds like you’re concerned about giving away our apprehensions about this journey, is that right? #EdrickChoice] 
#YaelTell
That’s right. I mean. Think about it. This kind of thing has never happened before. Fate-struck are <color=blue><b>usually friends</b></color>, or at least amiable. But her… I can’t stand her. 
    +++++ [Can you elaborate? #EdrickChoice]
    {YaelTell == 1: Don't poke at me, Edrick! I'm already frustrated.} 
    {YaelTell == 0: I don't want to talk about what's bothering me right now.} 
    {YaelTell == 2: It just makes me so frustrated, Edrick. I don't understand anything anymore.} 
    #Yael
    And don't you think it's weird that the Tether has chosen two people who despise each other?
    ++++++[That is strange, but the Tether has chosen you both for a reason. #EdrickChoice] 
    #YaelPensive
    That reason better be good, or I'm going to have come all this way for nothing.
        +++++++[Well, we'll see, I suppose. #EdrickChoice]
    ->COOLOFF
    ++++++[Why do you care? #EdrickChoice]
    #YaelPensive
    I had my entire life planned before this. I was <color=blue>hoping to be tethered to my beloved,</color> but that's a pipe dream now. I don't even know why I bothered falling in love if I was just going to be tied to an oaf.
    +++++++[I'm sorry, Yael. That must be tough to come to terms with. #EdrickChoice]
    #Yael
    Yes, but it feels good to get it out there.
    ++++++++[Continue #EdrickContinue]
    ~DecreaseTension(3)
    ->COOLOFF
//Option 3:
    *[Reacting rashly didn’t help our image either. #EdrickChoice]
    ~IncreaseYaelExhaust(1)
#Yael 
It was a rash decision, I won’t lie. But I was only trying to help. We can’t invite any <color=blue>bad omens</color> on our journey with unwashed hands.
++[What motivated you to go so far as to lay hands on her? #EdrickChoice]
#Yael
Well, I told her to put the hotcakes down and clean her hands, and it was as if she had cotton stuck in her ears. So I just. Acted on it.
	+++[Regardless of how put off you feel, Jasper is your colleague. It would be just as unfair of you to disrespect her space. #EdrickChoice]
#Yael
You do know I despise it when you start making sense, right Edrick?
++++[I think I can tell. #EdrickChoice] ->COOLOFF
	+++[I think you should apologize for making her drop the food. #EdrickChoice] 
#Yael
And I think you should stop telling me what to do, Edrick.
++++[Right, sorry. Maybe that's a sore subject. #EdrickChoice] 
#Edrick
It doesn't seem like Yael wants to make amends this way. Maybe there's something else I can say. 
+++++[Continue #EdrickContinue] 
~IncreaseTension(4)
-> COOLOFF
++++[Unfortunately my friend, that is my job. #EdrickChoice] ->COOLOFF
++ [I understand Moonwalkers have rituals they must perform before eating. Jasper shouldn’t have to abide by those. #EdrickChoice]
~SetYaelRitualTrue()
#Yael
Well, I shouldn’t have to witness her sacrilege at every other moment, either.
+++[Maybe you should look away next time? #EdrickChoice] ->COOLOFF
+++[Remember, it's not sacrilege to her. It's just eating food. #EdrickChoice]
#YaelPensive
Knowing this, I suppose I was acting a bit rashly. The Moonwalkers will <color=red>never accept a gift with dirty hands.</color> But Jasper is no cleric.
++++[It's alright to be a bit frustrated. How about we bring this up? #EdrickChoice] ->COOLOFF

//UNLOCKED ONLY BY TALKING TO JASPER ABOUT VOCAB FIRST.
*{jasperVocab} [I think Jasper is a little frustrated by your vocabulary. #EdrickChoice, #NewInfo]
~IncreaseYaelExhaust(1)
#Yael
It’s certainly not my fault. We do have state of the art education in the city. If she wanted to communicate with me respectfully, her tone would reflect that.
//OPTION 1
++[Yael, how will policing Jasper's tone get her to respect you? #EdrickChoice]
~IncreaseTension(3)
#YaelPensive
W-well, if she really cared about the status we hold, she would be more respectful!
+++[To you? Or to the townsfolk? #EdrickChoice]
#Yael
To me! If I'm going to be stuck here with her, I at least deserve some respect. Not some clown who won't stop poking fun at me.
++++[Right. #EdrickContinue]
#Edrick
I think I have everything I need from Yael... Looks like she's mostly miffed about Jasper's antics.
+++++[Continue #EdrickContinue]->COOLOFF


//OPTION 2
++[You're not exactly communicating respectfully yourself. #EdrickChoice]
#Yael
W-well, what do you mean by that? I'm using proper ettiqute, speaking clearly. Jasper's the one who can't keep her hands to herself.
+++[I have it in my notes here, that you called her an "oaf," "bloke," and "jester". #EdrickChoice] 
#YaelPensive
...yes.
++++[So I think you can tone it down a little bit. For the sake of the mission? #EdrickChoice]
#YaelPensive
...I. I suppose. 
+++++[Okay, now let me see about Jasper... #EdrickChoice]
->COOLOFF
   
->DONE

=JASPER
#Jasper
Why’s Yael so concerned with how prim and proper we look, anyways? We’re trudging through dirt. 
+[Can you share a bit more about this argument from your perspective, Jasper? #EdrickChoice]
	//~ChangeNotesIndex(1)
	//~UpdateNote()
#Jasper: 
There’s not much to share. I was offered food, I said “thank you,” and I tried to eat it, before Yael decided I should starve.
++{yaelRitual}[In Yael’s sect, hand washing is a ritual that cannot be forgone before eating. #EdrickChoice, #NewInfo]
#Jasper
I don’t really care what Moonwalkers do before eating. <i>We</i> don’t do that. 
+++[ You don’t have to do that. I’m just letting you know. #EdrickChoice] 
#Jasper
Well, why don’t you tell her that I’m not going to abide by her rules. So she can shove it wherever she shoves all those books she can't get her nose out of.
++++[...I may word that a bit differently. #EdrickChoice]
->COOLOFF
+++[Could you at least wipe or wash your hands before eating next time?]
~IncreaseTension(2)
#Jasper
Why are you asking me to compromise first? She’s literally insulting me left and right. I deserve some <color = blue>respect!</color> 
++++[I understand. I'll ask her about this. #EdrickChoice]
#Edrick
It seems like Jasper is very hurt by Yael's actions. Maybe I should talk to her - looks like this is something to take note of.
+++++[Continue #EdrickContinue]->COOLOFF
++[You seem quite irritated about this. May I ask why? #EdrickChoice]
#Jasper
<color=red>Ugh, few people enjoy being roughhoused</color>, Edrick. And besides that. She keeps saying things she knows I’m not going to understand.
+++[Like what? Elaborate? #EdrickChoice]
#Jasper
Can you believe she asked me to have a proper “decorum?” What does decorating anything have to do with this?
++++[…I see. Did she tell you what that actually means? #EdrickChoice]
#Jasper
No! And she’s been doing that on purpose - using fancy language just to complicate everything. 
+++++[How about we bring that up when we regroup? #EdrickChoice]
~SetJasperVocabTrue()
->COOLOFF
+[Never mind. #EdrickChoice] -> COOLOFF
+[I can imagine you're still pretty hungry. #EdrickChoice]
#Jasper
Yeah, I never got to eat my hotcake, man. All because Yael decided to crash out.
++[And then she started yelling. #EdrickChoice]
#Jasper
Yeah. I didn't mean to be offensive, but if she's going to be nasty, I'm gonna be nasty back. That's how things worked where I'm from, and I don't care if we're Tethered together or not.
+++[Where you're from? The Sunblades, right? #EdrickChoice]
#Jasper
That's right! My friends and I would always be pretty blunt with each other. That way, everything is laid bare. No secrets! The Sunblades don't have time to be two faced about anything.
++++[I see. Very different from Yael. #EdrickChoice]
#Jasper
Yeah, she doesn't really have friends, huh. Sad.
+++++[Well... I wouldn't go so far as to say that. #EdrickChoice] ->COOLOFF
+++[Do you feel like being nasty could contribute to a harsher work environment? #EdrickChoice]
#Jasper
~IncreaseTension(3)
Yael's gotta grow a thicker skin if a few crumbs throw her off. We can't let the mission be derailed by petty arguments, right? We're heroes. That's much more important.
++++[I agree, actually! The mission is more important. #EdrickChoice]
~DecreaseTension(4)
#Edrick
It seems like Jasper really holds her purpose above these arguments. Maybe that's something to take note of...
++++++[Back to the group, then. #EdrickChoice] ->COOLOFF
->DONE

=REGROUP
#Edrick
I think I know what's wrong. 
+[Continue #EdrickContinue] 
#Yael
And what is that, Edrick?
++[One of you needs to apologize.#EdrickChoice]
+++[Yael, you should apologize to Jasper. #EdrickChoice] ->YAELAPOLOGIES
+++[Jasper, you should apologize to Yael. #EdrickChoice] ->JASPAPOLOGIES
++[There is one thing you both value most. #EdrickChoice] ->CORECHOOSER
++[This is something you must figure out for yourselves. #EdrickChoice] 
#Jasper
Well when things get dicey, I tend to figure things out with my fists. Probably a completely foreign concept for soft hearted city folk.
+++[Wait a minute... #EdrickContinue]
~IncreaseTension(7)
#YaelAngry
Your threats are of no consequence to me, brute. You forget that the Tether would cause you the same grief if you were to lay your hands on me.
++++[This isn't working. #EdrickChoice]
#Jasper
Whatever. I'm done here. Let's go, Edrick. 
->DONE

=YAELAPOLOGIES
#YaelAngry
Well Jasper, our Calibrator has asked me to apologize to you, so I must say, I’m sorry for ever trying to teach you any manners. 
+[No, wait, not like that! #EdrickChoice]
~IncreaseTension(6)
#Jasper
Wow. Never before has anyone tested my patience like this. I need to start praying for more patience. 
++[Maybe that wasn't the right move... #EdrickChoice]
->COOLOFF
->DONE
=JASPAPOLOGIES
//if you ask Jasper to apologize
#Jasper
There is <b>no</b> way I'm apologizing to her.
+[Continue #EdrickContinue]
#Yael
Oh, that's okay, I didn't think you were capable of it.
~IncreaseTension(6)
++[Come on, you two... #EdrickContinue]
#Jasper
What is your problem? Seriously, tell me. 
+++[Alright, alright. Let's try again. #EdrickContinue] ->COOLOFF
->DONE

=CORECHOOSER
*[You're not communicating with each other about how you'd like to be respected. #EdrickChoice] ->RESPECT
*[You both miss your friends, and would like to be friends. #EdrickChoice] ->FRIENDS
*[You both value the mission more than anything else. #EdrickChoice] ->MISSIONSTATEMENT
+++[Unsure #EdrickChoice] -> COOLOFF
#Edrick
->DONE

=RESPECT
#Jasper
What do you mean, respect? I'm respectful! I'm plenty respectful!
+[Continue #EdrickContinue]
You both wish to be respected, both by each other and by those who depend on you during this quest. You just haven't communicated what will earn respect from each other; if you could be clear about that now, I'm sure this will be resolved.
#Edrick
++[Continue #EdrickContinue] 

~DecreaseTension(5)

A bit hard to say this, I’ll admit, but I was wrong, Jasper. I should not have gone to such lengths to prevent you from eating. Even if you were doing it in a weird, gross way. 
#YaelPensive
+++[Continue #EdrickContinue]

You know, that’s all I wanted to hear. 
Listen, Moonwalker. You use a lot of words that you’ve learned in a big city. I don’t understand all of them, and I’ve been getting frustrated. Could you slow down a bit or explain what you mean? 
#Jasper
++++[Continue #EdrickContinue]
#Yael
I suppose if it will ease our communications, I could be a bit more cognizant of - I mean. I can slow down. 
+++++[Looks like this is wrapping up nicely. #EdrickContinue]
#Edrick
I'm happy we could find something nice to say to each other! Now let's move on.
->DONE
=FRIENDS
#Jasper
Yael and I don't need to be friends for this to work. And furthermore, I wouldn't be friends with someone who can't go two seconds without insulting me. 
+[Continue #EdrickContinue]
#Yael
This journey isn't about making friends, Edrick. Did the gods send someone so daft to help us, really?
++[There must be something they can agree on... #EdrickContinue]
~IncreaseTension(3) 
->COOLOFF
->DONE
=MISSIONSTATEMENT
#Jasper
It doesn't matter who the Tether chose or how much I can't stand Yael, that's true. But That's not what I'm upset about! Yael is pissing me off! 
+[Oh, sorry, I must have misunderstood. #EdrickChoice]
#Yael
The next time we see a prayer stone, I'm going to pray the Tether is cut so I can die in peace.
++[There must be something they can agree on... #EdrickChoice]
~IncreaseTension(4)
->COOLOFF

==function YaelTellFalse(amount)
~YaelTell = 1
~return YaelTell

==function YaelTellTrue(amount)
~YaelTell = 2

==function IncreaseTension(amount)
~tension = tension + amount

==function DecreaseTension(amount)
~tension = tension - amount

==function SetJasperVocabTrue
~jasperVocab = true

==function SetYaelRitualTrue
~yaelRitual = true

==function ChangeNotesIndex(amount)
~NotesIndex = amount

==function ExtendedTutorial(amount)
~ExternalTutorialNum = amount

==function IncreaseYaelExhaust(amount)
~YaelExhaust = YaelExhaust + amount
EXTERNAL UpdateNote()

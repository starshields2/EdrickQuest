VAR core = 0
VAR needs = 0
VAR bounds = 0
VAR violations = 0
VAR commonalities = 0
VAR YaelTell = 0
VAR medPoints = 0
VAR tension = 10
VAR highTen = false
VAR jasperVocab = false
VAR NotesIndex = 0

=== notes ===
- "First note text"
- "Second note text"
- "Third note text"

#Jasper 
You’re not <b>seriously</b> stopping us for this, are you? We’ve already lost precious time. If we are late for the <color=orange> Ritual </color>, we’ll have set out for nothing. 
+[I just want everyone to get along.]

#Yael
If Edrick won’t stop us for this, I will. No <color=orange>Fate-struck</color> person should be as ill mannered and gross as you’re acting right now, especially not during the Ritual.
++[Continue]

#Jasper
I’m ill mannered? You made me drop food onto the ground! 
+++[Continue]

#Yael
I had to act quickly, before you ruined the food by inhaling it!
++++[Continue]

#Jasper
It’s food, that’s what you’re supposed to do! 
+++++[Continue]
#Edrick
Calm yourselves. Please confide in me, so I can help you two walk in each others’ shoes.
++++++[Continue] -> COOLOFF

=COOLOFF
#Edrick
{jasperVocab}
+[Talk to Yael]-> Yael
+[Talk to Jasper] ->JASPER
+[Regroup]  -> REGROUP
->DONE

=Yael
#Yael
You don’t understand, Edrick. She’s making us look like idiots out there. 
+[Why do you think that?]

#Yael
We are not average people. Not <i>anymore</i>. Our decorum around villagers should reflect that. How can they look up to us as Fate-struck if we’re taking handouts from everyone we cross?
	++[Are they handouts, or offerings of goodwill?]
#Yael
What’s the difference? We shouldn’t be so eager to show that we are fragile. They may lose faith in us - it’s better to kindly refuse a gift than to show dependence on it.
	+++[Our gods have chosen us for a reason. Any god fearing person will also have faith in us.]
	~ChangeNotesIndex(2)
	~UpdateNote()
#YaelPensive
Well, that’s certainly true. Though it does little to ease my nerves.
	++++[It sounds like you’re concerned about giving away our apprehensions about this journey, is that right?] 
#YaelTell
That’s right. I mean. Think about it. This kind of thing has never happened before. Fate-struck are <color=blue>usually friends</color>, or at least amiable. But her… I can’t stand her. 
    +++++ [Can you elaborate?]
    {YaelTell == 1: Don't poke at me.} 
    {YaelTell == 2: Maybe there's something more to this?} 
    #Yael
    Well, don't you think it's weird that the Tether has chosen two people who despise each other?
    ++++++[That is strange, but the Tether has chosen you both for a reason.] ->COOLOFF
    ++++++[Why do you care?]
    #YaelTell
    I had my entire life planned before this. I was hoping to be tethered to my beloved, but that's a pipe dream now. I don't even know why I bothered falling in love if I was just going to be tied to an oaf.
    +++++++[I'm sorry, Yael. That must be tough to come to terms with.]
    #Yael
    Yes, but it feels good to get it out there.
    ++++++++[Continue]
    ~DecreaseTension(3)
    ->COOLOFF
    
    +[Reacting rashly didn’t help our image either.]
#Yael 
It was a rash decision, I won’t lie. But I was only trying to help. We can’t invite any bad omens on our journey with unwashed hands.
++[What motivated you to go so far as to lay hands on her?]
#Yael
Well, I told her to put the hotcakes down and clean her hands, and it was as if she had cotton stuck in her ears. So I just. Acted on it.
	+++[Regardless of how put off you feel, Jasper is your colleague. It would be just as unfair of you to disrespect her space.]
#Yael
You do know I despise it when you start making sense, right Edrick?
++++[I think I can tell.] ->COOLOFF
	***[I think you should apologize for making her drop the food.] 
#Yael
And I think you should stop telling me what to do, Edrick.
++++[Right, sorry. Maybe that's a sore subject.] 
#Edrick
It doesn't seem like Yael wants to make amends this way. Maybe there's something else I can say. 
+++++[Continue] 
~IncreaseTension(4)
-> COOLOFF
++++[Unfortunately my friend, that is my job.] ->COOLOFF
++ [I understand Moonwalkers have rituals they must perform before eating. Jasper shouldn’t have to abide by those.]
#Yael
Well, I shouldn’t have to witness her sacrilege at every other moment, either.
+++[Maybe you should look away next time?] ->COOLOFF
+{jasperVocab} [I think Jasper is a little frustrated by your vocabulary.]
#Yael
It’s certainly not my fault. We do have state of the art education in the city. 

   
->DONE

=JASPER
#Jasper
Why’s Yael so concerned with how prim and proper we look, anyways? We’re trudging through dirt. 
+[Can you share a bit more about this argument from your perspective, Jasper?]
#Jasper: 
There’s not much to share. I was offered food, I said “thank you,” and I tried to eat it, before Yael decided I should starve.
++[In Yael’s sect, hand washing is a ritual that cannot be forgone before eating. Doing so could invite chaos and ill-intended spirits from the hands into the body.]
#Jasper
I don’t really care what Moonwalkers do before eating. We don’t do that. 
+++[ You don’t have to do that. I’m just letting you know.] 
#Jasper
Well, why don’t you tell her that I’m not going to abide by her rules. So she can shove it wherever she shoves all those books she can't get her nose out of.
++++[Uhh... how about we loop back around to this?]
~IncreaseTension(2)
->COOLOFF
+++[Could you at least wipe or wash your hands before eating next time?]
#Jasper
Why are you asking me to compromise first? She’s literally insulting me left and right. I deserve some respect! 
++++[I understand. I'll ask her about this.]
#Edrick
It seems like Jasper is very hurt by Yael's actions. Maybe I should talk to her.
~IncreaseTension(2)
+++++[Continue]->COOLOFF
++[You seem quite irritated about this. May I ask why?]
#Jasper
Few people enjoy being roughhoused, Edrick. And besides that. She keeps saying things she knows I’m not going to understand.
+++[Like what? Elaborate?]
#Jasper
Can you believe she asked me to have a proper “decorum?” What does decorating anything have to do with this?
++++[…I see. Did she tell you what that actually means?]
#Jasper
No! And she’s been doing that on purpose - using fancy language just to complicate everything. 
+++++[How about we bring that up when we regroup?]
~SetJasperVocabTrue()
->COOLOFF

->DONE

=REGROUP
#Edrick
I think I know what's wrong. 
+[Continue] 
#Yael
And what is that, Edrick?
++[Yael, you should apologize to Jasper.] ->YAELAPOLOGIES
++[Jasper, you should apologize to Yael.] ->JASPAPOLOGIES
++[There is one thing you both value most.] ->CORECHOOSER
++[This is something you must figure out for yourselves.] -> COOLOFF

=YAELAPOLOGIES
//(If you asked Yael to apologize)
#YaelAnger
Well Jasper, our Calibrator has asked me to apologize to you, so I must say, I’m sorry for ever trying to teach you any manners. 
+[No, wait, not like that!] 
#Jasper
Wow. Never before has anyone tested my patience like this. I need to start praying for more patience. 
++[Maybe that wasn't the right move...]
->DONE
=JASPAPOLOGIES
//if you ask Jasper to apologize
#Jasper
There is <b>no</b> way I'm apologizing to her.
+[Continue]
#Yael
Oh, that's okay, I didn't think you were capable of it.
++[Come on, you two...]
#Jasper
What is your problem? Seriously, tell me. 
->DONE

=CORECHOOSER
#Edrick
***[Respect] ->RESPECT
***[Friendship] ->FRIENDS
***[The mission] ->MISSIONSTATEMENT
+++[Unsure] -> COOLOFF

=RESPECT
#Edrick
You both wish to be respected, both by each other and by those who depend on you during this quest. You just haven't communicated what will earn respect from each other; if you could be clear about that now, I'm sure this will be resolved.
+[Continue] 
~DecreaseTension(5)
//(If you pointed out to Yael that she should respect Jasper’s space)
#YaelTell
A bit hard to say this, I’ll admit, but I was wrong, Jasper. I should not have gone to such lengths to prevent you from eating. Even if you were doing it in a weird, gross way. 
++[Continue]
#Jasper
You know, that’s all I wanted to hear. 

//(If you asked Jasper to express her frustrations)
Listen, Moonwalker. You use a lot of words that you’ve learned in a big city. I don’t understand all of them, and I’ve been getting frustrated. Could you slow down a bit or explain what you mean? 
+++[Continue]
#Yael
I suppose if it will ease our communications, I could be a bit more cognizant of - I mean. I can slow down. 
++++[Looks like this is wrapping up nicely.]
#Edrick
I'm happy we could find something nice to say to each other! Now let's move on.
->DONE
=FRIENDS
#Jasper
Yael and I don't need to be friends for this to work. And furthermore, I wouldn't be friends with someone who can't go two seconds without insulting me. 
+[Continue]
#Yael
This journey isn't about making friends, Edrick. Did the gods send someone so daft to help us, really?
++[There must be something they can agree on...]
~IncreaseTension(2) 
->COOLOFF
->DONE
=MISSIONSTATEMENT
#Jasper
It doesn't matter who the Tether chose or how much I can't stand Yael, that's true. But That's not what I'm upset about! Yael is pissing me off! 
+[Oh, sorry, I must have misunderstood.]
#Yael
The next time we see a prayer stone, I'm going to pray the Tether is cut so I can die in peace.
++[There must be something they can agree on...]
~IncreaseTension(1)
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

==function ChangeNotesIndex(amount)
~NotesIndex = amount

EXTERNAL UpdateNote()

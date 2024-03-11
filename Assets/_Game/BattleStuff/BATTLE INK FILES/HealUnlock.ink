VAR YPoints = 0
VAR JPoints = 0
VAR EdrickPoints = 15


#Yael
What? What have I done wrong now? Is that armor not to protect you? It's clearly defective.
+[Continue] ->con1
=con1
#Jasper
Heal me, damn you! I'm your muscle!
+[Continue]->con2
->DONE
=con2
#Yael
Apologize for touching my staff. 
+[Continue] ->con3
->DONE
=con3
#Jasper
Are you serious?!
+[Continue] ->MEDIATE
->DONE


=MEDIATE
#Edrick
I should be compensated for my marriage counseling services.
+[Heal Jasper!] Quickly now, use the tonic!
{YPoints >= 5: 
    I suppose I judged you too quickly Jasper. I'll lend you my moonlight. -> END
}
{YPoints < 5: 
    Absolutely not.   
    +[Okay, let's try this again.] -> MEDIATE
} 

+[Jasper, say sorry!] Just say you're sorry and get it over with.
{JPoints >= 6: 
    Alright, I'm sorry. I didn't know how important this was to you. -> END
}
{JPoints < 6: 
    What? Why are you asking me to apologize? 
    +[Maybe we need to talk this out some more.]->MEDIATE
}


+[What's the problem here?] 
#Edrick
So what exactly is the issue?
++[Continue]
#Yael
She got grease all over my staff! This is high quality angel wood.
***[Is now the time for petty apologies?]
~increaseJMorale(2)
~decreaseYMorale(1)
#Yael
Of course it is, this is-
++++[High quality angelwood, yes.]
#Yael
Yes you understand! So it can't be soiled. Problem solved, say sorry Jasper!
+++++[Continue]->JaspRespond1
***[I understand staff hygiene, but...]
I understand you want to keep the staff clean, but this is a pressing issue.
~increaseYMorale(2)
~decreaseJMorale(1)
#Yael
It's not just about hygiene. The angelwood is not to be touched by outsiers. 
++++[Continue]->JaspRespond1
=JaspRespond1
#Jasper
So our germs are too disgusting for you too?
+++++[Continue] ->YRespond1
->DONE

=YRespond1
#Yael
Yes actually, my father told me this directly; dirt will sully the magic. Especially dirt from those who walk in the sun.
*****[It's twilight.]
#Edrick 
Well Technically you are walking in the sun right now. It's twilight.
******[Continue]
#Yael
What are you suggesting? That I've already dirtied the staff?
+++++++[Absolutely.] 
~decreaseYMorale(3)
#Yael
So what do I do then?
++++++++[Maybe it's time to try something new.]
#Yael
Like what? We're in the middle of nowhere. Not a single magistrate in sight. How can I resolve this?
+++++++++[Maybe Jasper can help you clean it.]
#Jasper
Yeah, I'll clean it if that will help us move the hell on.
++++++++++[Great.] ->MEDIATE
~increaseJMorale(5)
++++++++[Forget it, and try the magic anyway.]
#Yael
You may as well ask me to drive a stake through my heart.
~decreaseYMorale(2)
+++++++++[Ouch.] ->MEDIATE

*******[Let her down easy.]
#Edrick
Well just think about it. The sun is still up. You're walking in it right now.
++++++++[Continue]
#Yael
But I'm no outsider. Jasper is. 
********[What if Jasper helped you clean it up?]
~increaseYMorale(10)
#Yael
I suppose that could work! A temporary solution, but one nonetheless.
+++++++++[As long as it gets us moving.] ->MEDIATE
->DONE
****[Yael's Father?]
#Edrick 
And without walking in the sun himself, your father knew this... how?
#Yael
He's a scholarly man; he's studied these effects personally. Clearly he knows what he's talking about. 
++++++[Continue] ->JaspRespond2
=JaspRespond2
#Jasper
Dawnkeepers possess no magic! Think! How can our natural selves repel your spells?
+[Continue] ->con7

=con7
#Yael
You wouldn't know anything about that, your people don't study magic.
*[Well there you have it, Jasper. Say sorry.] 
~decreaseJMorale(4)
Oh great, now I'm dirty AND stupid, thanks Edrick.
#Jasper
++[Continue] ->MEDIATE

*[Actually...] I've dabbled in Dawnkeeper studies myself- they do study magic. And there's nothing that supports your father's words.
~increaseYMorale(3)
#Yael
...I suppose, given the circumstances, I should give it a shot.

++[Good. Moving on.]->MEDIATE

->DONE
->DONE

==function increaseYMorale(amount)
~YPoints = YPoints + amount

==function decreaseEdrickPoints(amount)
~EdrickPoints = EdrickPoints - amount

==function decreaseYMorale(amount)
~YPoints = YPoints -1

==function increaseJMorale(amount)
~JPoints = JPoints + amount

==function decreaseJMorale(amount)
~JPoints = JPoints -1


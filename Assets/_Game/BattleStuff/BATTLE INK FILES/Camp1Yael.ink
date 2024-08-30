VAR YMorale = 0
VAR JPoints = 0
VAR medPoints = 100


->BEGINNING
=BEGINNING
You had something to say? #Yael

+[Talk] -> START
+[Gift] -> GIFT
+[Leave] ->DONE
->DONE
=GIFT
#Yael
Oh, for me?
*[Moonshard]
~increaseYMorale(3) 
Ah. We used to collect these by the lakeshore and turn them into bracelets.
(You have {YMorale} morale btw)
++[I'm glad you like it.] ->GIFT
*[Rock]
~decreaseYMorale(3)
What am I supposed to do with this?
(You have {YMorale} morale btw)
++[I don't know.] ->GIFT
+[Nevermind.] ->START

->DONE
=START
#Yael
Well, what is it then?
**[If you didn't want to travel with Jasper, why are you even here?]
~increaseYMorale(1)
#Yael
Do you take me for a fan of the apocalypse? I'd rather work with a country bumpkin than watch my people suffer.
***[Really?]
Yes, really. The equinox has no preference for sun or moon. It will destroy all, unless we destroy it.
****[Do you think we can do it?]
Of course I do. I know I can lead us to a favorable outcome, if Jasper listens to me.
*****[I believe in you.]
~increaseYMorale(3)
(You have {YMorale} morale btw)
No need to believe in something that is factual. If you trust me, you simply know it. 
->DONE
*****[Are you sure?]
~decreaseYMorale(2)
Why are you prodding at me like a horse? Time will tell. Now go to bed.
(You have {YMorale} morale btw)
->DONE
***[Country bumpkin?] 
(You have {YMorale} morale btw)
->DONE
**[How will you prepare for the journey ahead?]
(You have {YMorale} morale btw)
#Yael
The Goddess will bless me and keep me, all I must do is give myself to her.
***[You seem rather devoted.]
~increaseYMorale(3)
I pride myself in that. I am to be the next Cresent-maiden after all - and a priestess that strays from the path is no priestess at all. 
(You have {YMorale} morale btw)
->DONE
***[Give yourself to her? Doesn't that sound a bit... strange?]
#Yael
It is our way. Moonwalkers can't see themselves as people, only vessels for her will. You will do well to not insult my tradition.
(You have {YMorale} morale btw)
****[Well, what's her will right now?]
To go to bed, obviously. 
(You have {YMorale} morale btw)
->DONE

**[What will you do in your free time?]
#Yael
(You have {YMorale} morale btw)
Most likely I will return to prayer. I don't find anything here endearing enough to go outside. It's just gross. 
->DONE

->END

==function increaseYMorale(amount)
~YMorale = YMorale + amount
==function decreaseYMorale(amount)
~YMorale = YMorale - amount
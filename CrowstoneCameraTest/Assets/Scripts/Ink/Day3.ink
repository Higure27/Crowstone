VAR day_Complete = false
VAR met_Pemberton = false
VAR adelaida_ran = false
VAR Currency = 0
VAR heared_rumor =false


->Pemberton

===Pemberton===

{met_Pemberton: ->second_meeting|->first_meeting}

=first_meeting
Sheriff, good morning! I'm glad you're here. My suspicions were correct. There's a woman in town, Adelaida Santorez. She knows about me. I need you to guarantee her silence.
    *[Do you have any investigation tips for her?]
        She seems the type that might be bribed. You can try that in addition to your other skills. I trust you can help me with her, Sheriff. You don't seem to be the rich type so let me give you a small loan *recive $500*
            ~ Currency = 500 
                **[I'm on it]
                    ~ met_Pemberton = true
                    ->DONE    
    *[I'm on it]
        ~ met_Pemberton = true
        ->DONE
=second_meeting
Sheriff, you were successful in silencing Adelaida?
+[Still working on it]
    ->DONE
*{adelaida_ran}[Yes, she won't be asking about you anymore.]
                I knew I could count on you. Thank you for all your help.
                **[It's no problem.]
                    ~day_Complete = true
                    ->DONE
                    
===Bartender===

->intial

=intial
Sheriff, I trust you're not here to arrest anyone. What can I do for you?->talk
=talk
+ [Can I get a drink?]
    {Currency > 50: ->get_drink|I don’t think you can afford one right now. ->talk}
+[What can you tell me about Adelaida?]
    You're nosy, aren't you? I guess I can help you out. What do you need to know?
    ->investigate
+[I'm good for now]
    ->DONE
=investigate
*[What is she like?]
     Are you serious? Haven't you seen her flirting around with every man in town? Ever since her husband left her for that other woman, she's become a jealous vixen. -> investigate
*[Have you heard any rumors about her?]
    ~heared_rumor=true
    Only that she's desperate to get out of town. Something about moving to San Francisco and marrying a rich man. Don't know how she's going to do it if she keeps bouncing around the men here. None of them are taking her there, that's for sure. You know, I think I saw her giving something to Albert Moorsby in the mercantile store. In fact, she's been giving a lot of things to a lot of the men. Might try talking to Albert. I'll bet it's a love token or something. If that's true, she's cheating on all her suitors, the harlot! ->investigate
+[I think that's enough for now]
    ->DONE
    

=get_drink 
 ~Currency = Currency-50
 Don't over due it Sheriff.
 Anything else I can do for you?
 ->talk
 
===Adelaida===
Well, hello there, Sheriff. What's a fine man like yourself doing out here in this heat?
->DONE


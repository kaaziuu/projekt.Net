window.onload = () =>
{
    dayOfWeek = {
        "1":"Poniedziałek",
        "2":"Wtorek",
        "3":"Środa",
        "4":"Czwartek",
        "5":"Piątek",
    }
    const el = document.getElementsByClassName("dayOfWeek");
    console.log(el);
    for (let i=0; i<el.length;i++)
    {
        let day = el[i].innerHTML;
        
        console.log(day);
        el[i].innerHTML = dayOfWeek[day];

    }
}
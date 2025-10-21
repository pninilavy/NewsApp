// קוד JS שמטפל בלחיצה על כרטיסי חדשות וטוען את גוף הכתבה בעזרת AJAX
$(document).ready(function () {


    // הוספת מאזין ללחיצה על כרטיס חדשות

    $(document).on('click', '.news-card', function () {
        const $card = $(this);
        const id = $card.data('id');
        const $body = $card.find('.article-body');

        //סגירת הכתבה במקרה שכבר פתוח
        if ($card.hasClass('open')) {
            $body.slideUp(200);
            $card.removeClass('open');
            return;
        }

        //סגירת כל הכתבות הפתוחות
        $('.news-card.open').removeClass('open').find('.article-body').slideUp(200);

        //לשרת כדי להביא את הכתבה המלאה AJAX שליחת בקשת
        $.ajax({
            type: "POST",
            url: "Default.aspx/GetArticle",
            data: JSON.stringify({ id: id }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d) {
                    const item = JSON.parse(response.d);
                    const html = `
                        <p>${item.Description}</p>
                        <a href="${item.Link}" target="_blank" class="btn btn-outline-danger btn-sm">
                            לכתבה המלאה
                        </a>
                    `;
                    $body.html(html).slideDown(300);
                    $card.addClass('open');
                }
            },
            error: function () {
                alert("שגיאה בטעינת הכתבה.");
            }
        });
    });
});

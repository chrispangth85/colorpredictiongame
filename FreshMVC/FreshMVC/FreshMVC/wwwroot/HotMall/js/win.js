$('.money-numbers h2').click(function() {
    $('.money-numbers h2').removeClass('active-number')
    $(this).addClass('active-number')

    var value = parseInt($(this).text()) * $('.qty-number').val()
    $('.total-contract-money').text(value)
})

$('.quantity-btn').on('click', function() {
    var activeQty = $(this).closest('.modal').find('.money-numbers .active-number');

    var qtyNumber;
    if($(this).hasClass('plus-btn')) {
        qtyNumber  = parseInt($('.qty-number').val()) + 1
    } else {
        qtyNumber = parseInt($('.qty-number').val()) - 1
    }
    var value = parseInt(activeQty.text()) * (qtyNumber)
    $('.total-contract-money').text(value)
})

$('.clock-dummy-btn').click(function() {
    $('.continue-btn').toggleClass('continue-enable')
    $('.clock-count-down').toggleClass('clock-disable')
})

$('.join-dummy-btn').click(function() {
    $('.allbtnable').toggleClass('allbtndisable')
})


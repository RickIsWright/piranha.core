Vue.component('rawhtmlcontent-block', {
    props: ['uid', 'model'],
    template: '<div class="block-body"><textarea :id="uid" rows="8" v-model="model.body.value"></textarea></div>'

});